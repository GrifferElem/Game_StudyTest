using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="ConversationData",menuName ="MyScriptableObject/ConversationSO",order =1)]
public class ConversationSO : ScriptableObject
{
    public List<Sentence> sentences = new List<Sentence>();
}
