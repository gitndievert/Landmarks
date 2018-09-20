using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SBK.Unity;

public class NodeManager : PSingle<NodeManager>
{
    public List<Node> NodeList;
    
    protected override void PAwake()
    {
        if (NodeList != null && NodeList.Count == 0)
            NodeList.AddRange(transform.GetComponentsInChildren<Node>());
    }

    protected override void PDestroy()
    {

    }   
}
