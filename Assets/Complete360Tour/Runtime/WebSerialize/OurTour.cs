using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DigitalSalmon.C360;
using UnityEngine;

public static class OurTour
    {
        public static Tour GetFromJson(string json)
        {
            Debug.Log("Starting our Diserialization");

            if (string.IsNullOrEmpty(json) || json == " ")
                return new Tour((List<NodeData>) null);


            List<NodeData> list = JsonUtility.FromJson<List<NodeData>>(json);;
            Tour tour = new Tour(list);  
            Debug.Log("Successfully desirialized");

            if (tour == null)
                return new Tour((List<NodeData>) null);

            Debug.Log("Trying to UI Referency");
            UpdateUidReferences(tour);
            Debug.Log("Calling on Desialize");
            OnDeserialize(tour);
            Debug.Log("Returning value");

            return tour;
        }
        
        private static void OnSerialize(Tour tour)
        {
            foreach (NodeData nodeData in tour.NodeDataList)
                nodeData.OnSerialize();
            tour.NodeDataList.RemoveAll((Predicate<NodeData>) (n => !n.IsValid()));
        }

        private static void OnDeserialize(Tour tour)
        {
            foreach (NodeData nodeData in tour.NodeDataList)
                nodeData.OnDeserialize();
            tour.NodeDataList.RemoveAll((Predicate<NodeData>) (n => !n.IsValid()));
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

    }


