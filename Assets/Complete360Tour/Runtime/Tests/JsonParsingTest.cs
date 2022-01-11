using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DigitalSalmon.C360;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class JsonParsingTest
{
    private string _text;
    private Tour _tour;

    private string _element =
        "                {\n \"TargetNodeUID\": -414708992,\n \"MappedPosition\": {\n \"x\": 0.6138855,\n \"y\": 0.4889026\n },\n \"IconIndex\": 0,\n \"$type\": \"DigitalSalmon.C360.HotspotElement\" } ";

    private string _textAsset = "Assets/0Tours/Kosiv/Kosiv.txt";

    [SetUp]
    public void SetUp()
    {

        _text = AssetDatabase.LoadAssetAtPath<TextAsset>(_textAsset).text;
        _tour = TourConverter.ParseTour(_text);
    }
    // A Test behaves as an ordinary method
    [Test]
    public void Test0LoadingFile()
    {
         _text = AssetDatabase.LoadAssetAtPath<TextAsset>(_textAsset).text;
        Debug.Log("Loaded text \n"+_text);
        Assert.IsNotEmpty(_text,_text);
    }
    [Test]
    public void Test1GetNodeDataList()
    {
         JsonTextReader reader = new JsonTextReader(new StringReader(_text));
         StringBuilder sb  = new StringBuilder();

          while (reader.Read())
          {
              if (reader.Value != null)
              {
                  sb.AppendFormat("Token: {0}, Value: {1}\n", reader.TokenType, reader.Value);
              }
              else
              {
                  sb.AppendFormat("Token: {0}\n", reader.TokenType);
              }
          }

          string result = sb.ToString();
        Debug.Log(result);
        Assert.IsNotEmpty(result);
    }
    [Test]
    public void Test2ParseArray()
    {
        JObject o = JObject.Parse(_text);

        JArray a = (JArray)o["NodeDataList"];
        Debug.Log($"In our JSON Array we have {a.Count}");
        


        Assert.IsNotEmpty(a);

    }

    [Test]
    public void Test3TourConvert()
    {
        _tour = TourConverter.ParseTour(_text);


        Assert.IsNotEmpty(_tour.NodeDataList,$"We have {_tour.NodeDataList.Count} elements ");
    }
    [Test]
    public void Test4ImageNodeData()
    {
        NodeData data = _tour.NodeDataList[0];
        Assert.IsInstanceOf<ImageMediaNodeData>(data);

    }

    [Test]
    public void Test5ImageParamsCheck()
    {
        ImageMediaNodeData data = _tour.NodeDataList[0] as ImageMediaNodeData;
        string path = data.ResourcePath;
        Assert.IsTrue(path.Length>3,$"{path} has length less then 3");

        Assert.IsTrue(data.Uid!=0,"Uid is 0");
        Assert.IsTrue(data.NodePosition!=Vector2.zero&&data.NodePosition!=null,"Position is zero");
        Assert.IsTrue(data.EntryYaw!=0,"Yaw is 0");

        Assert.IsNotEmpty(data.NiceName,"Name is empty");
 
     }

    [Test]
    public void Test6ElementsParserCheck()
    {
        Debug.Log($"Element is {_element}");
        HotspotElement element = TourConverter.ConvertHotspotElement(JObject.Parse( _element));

        Assert.IsInstanceOf(typeof(HotspotElement),element);
        Assert.IsTrue(element.MappedPosition!=Vector2.zero,"Position is zero");
 
    }

    [Test]
    public void Test7HotspotElement()
    {
        List<INodeDataElement> elements = new List<INodeDataElement>();
        foreach (var node in _tour.NodeDataList)
        {
            elements.AddRange(node.Elements);
        }
        Assert.IsNotEmpty(elements);
        foreach (var element in elements)
        {
            Assert.IsInstanceOf<HotspotElement>(element);
            Assert.IsNotNull(((HotspotElement)element).TargetNodeData); 
        } 

    }

    [Test]
    public void Test8ElementList()
    {
        foreach (var node in _tour.NodeDataList)
        {
            Debug.Log(node.NiceName);
        }

    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator JsonParsingTestWithEnumeratorPasses()
    {
        yield return null;
    }
}