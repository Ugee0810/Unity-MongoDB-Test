using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;

// GameData.cs - 구조체로 콜렉션의 내용을 추가
public class GameData
{
    // MongoDB 내에서 관리하는 객체
    public ObjectId id { get; set; }  // 필드의 고유 Index
    public string name { get; set; }
    public int score { get; set; }
}
