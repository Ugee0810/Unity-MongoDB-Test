using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;

// GameData.cs - ����ü�� �ݷ����� ������ �߰�
public class GameData
{
    // MongoDB ������ �����ϴ� ��ü
    public ObjectId id { get; set; }  // �ʵ��� ���� Index
    public string name { get; set; }
    public int score { get; set; }
}
