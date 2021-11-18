using System;
using System.Collections.Generic;
using System.Linq;
using DigitalSalmon.C360;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class TourConverter 
{
    public static Tour ParseTour(string json)
    {
        JObject o = JObject.Parse(json); 
        JArray a = (JArray)o["NodeDataList"];

        List<NodeData> list = new List<NodeData>(a.Count); 
        

        for(int i = 0; i < a.Count; i++)
        {

            JToken jsonNode = a[i];

            var node = ParseNodeData(jsonNode);

            list.Add(node); 
        }

        Tour result = new Tour(list);
        UpdateUidReferences(result);

        return result;
    }

    private static void UpdateUidReferences(Tour tour)
    {
        if (tour.NodeDataList == null || tour.NodeDataList.Count == 0)
            return;
        foreach (ITargettedElement targettedElement in tour.NodeDataList.SelectMany<NodeData, ITargettedElement>((Func<NodeData, IEnumerable<ITargettedElement>>) (d => (IEnumerable<ITargettedElement>) d.GetElements<ITargettedElement>())).ToList<ITargettedElement>())
        {
            NodeData target = tour.DataFromUid(targettedElement.TargetNodeUid);
            if (target != null)
                targettedElement.AssignTarget(target);
        }
    }

    static string  _imageNodeType = "DigitalSalmon.C360.ImageMediaNodeData";

    private static NodeData ParseNodeData(JToken jsonNode)
    {
         string currentType = jsonNode["$type"].ToString();

         if (currentType == _imageNodeType)
         {
             var result = new ImageMediaNodeData();
            result.SetStereoscopic(jsonNode["IsStereo"].Value<bool>());

            string? path = jsonNode["ResourcePath"].Value<string>();

            result.AssignResourcePath(path);


            result.SetEntryYaw(jsonNode["EntryYaw"].Value<float>());

            SetPrivateFieldsImageNode(jsonNode, result);

            Vector2 pos = GetVector2(jsonNode["NodePosition"]);
 
            result.SetPosition(pos);

            var jsonElements = (JArray)jsonNode["Elements"];
            
            foreach (var element in jsonElements)
            {

                var hotsoptElement = ConvertHotspotElement(element);
                bool isAdded = result.AddElement(hotsoptElement);
                if (!isAdded)
                {
                    throw new Exception($"Can't add this element {hotspotElement.ToString()}");
                }

            } 

            return result;

         }

         throw new Exception($"{currentType} Is invalid NodeData type. Can't parse"); 

    }

    private static void SetPrivateFieldsImageNode(JToken jsonNode, ImageMediaNodeData result)
    {
        var uid = typeof(NodeData).GetField("_uid", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var name = typeof(NodeData).GetField("_name", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var mediaName = typeof(MediaNodeData).GetField("_mediaName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        int uidValue = jsonNode["UID"].Value<int>();
        string nameValue = jsonNode["Name"].Value<string>();
        string mediaValue = jsonNode["MediaName"].Value<string>();


        if (uid == null)
            throw new Exception("Private UID isn't set");

        uid.SetValue(result, uidValue);
        name.SetValue(result, nameValue);
        mediaName.SetValue(result, mediaValue);
    } 

    private static Vector2 GetVector2(JToken jToken){
        float x= jToken["x"].Value<float>();
        float y= jToken["y"].Value<float>();
        return new Vector2(x,y);
    }
    /*
     {
 "TargetNodeUID": -414708992,
 "MappedPosition": {
 "x": 0.6138855,
 "y": 0.4889026
 },
 "IconIndex": 0,
 "$type": "DigitalSalmon.C360.HotspotElement" 
    }*/
    public static readonly string hotspotElement ="DigitalSalmon.C360.HotspotElement";

        public static HotspotElement ConvertHotspotElement(JToken jsonNode) 
        {
 
         string currentType = jsonNode["$type"].ToString();

         if (currentType == hotspotElement)
         {
             var result = new HotspotElement();

             var index = jsonNode["IconIndex"].Value<int>();

            result.SetIconIndex(index);
 
            Vector2 pos = GetVector2(jsonNode["MappedPosition"]);
 
            (result as IMappedElement).SetMappedPosition(pos);


            int targetNodeUID = jsonNode["TargetNodeUID"].Value<int>();
            result.AssignTarget(targetNodeUID);

            return result;

         }

         throw new Exception($"{currentType} Is invalid NodeData type. Can't parse"); 

        }

}