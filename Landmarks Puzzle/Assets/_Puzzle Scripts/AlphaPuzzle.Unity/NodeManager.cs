using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SBK.Unity;

public class NodeManager : PSingle<NodeManager>
{
    public List<Node> NodeList;
    public GameObject SelectedEffect;
    public AudioClip LetterComplete;

    protected override void PAwake()
    {
        if (NodeList != null && NodeList.Count == 0)
            NodeList.AddRange(transform.GetComponentsInChildren<Node>());
    }

    protected override void PDestroy()
    {

    }

    public Node PullMapLetter(string letter)
    {
        return NodeList.FirstOrDefault(l => l.name.ToUpper() == letter.ToUpper());
    }
}
