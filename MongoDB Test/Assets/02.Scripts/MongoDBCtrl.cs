/// <summary>
/// Ŭ���̾�Ʈ ���� �� �����ͺ��̽�
/// �����ͺ��̽� ���� �� �ݷ���
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDBCtrl : MonoBehaviour
{
    // Get Connect(User) Info
    const string MONGO_URI =
        "mongodb+srv://song:song@cluster0.qs1ppkp.mongodb.net/?retryWrites=true&w=majority";
    MongoClient client;

    // Get Database Info
    const string DATABASE_NAME = 
        "TestDB";
    IMongoDatabase db;

    // - �������� �ݷ����� �ϳ��̹Ƿ� ���������� ����
    // - DB �ȿ��� ���� �ݷ����� ���� �� ���� �Լ����� �������°� ����
    // - GameData Class�� ���·� ����
    IMongoCollection<GameData> db_col;

    private void Start()
    {
        First();
        Debug.Log("=====:Action:=====");
        //DB_Find("song");
        //DB_All_View();
        //DB_Insert("ugi", 1);
        //DB_Remove("song");
    }

    void First()
    {
        DB_Login();
        Get_DataBase();
        Get_Collection();

        Debug.Log("=====:���� ����:=====");
        Debug.Log("Client : " + client);
        Debug.Log("DataBase Name : " + db);
        Debug.Log("Data Collection : " + db_col);
    }
    void DB_Login()       { client = new MongoClient(MONGO_URI); }
    void Get_DataBase()   { db = client.GetDatabase(DATABASE_NAME); }
    void Get_Collection() { db_col = db.GetCollection<GameData>("TestDB.col"); }

    void DB_Insert(string name, int score)
    {
        // MongoDB ������ name�ʵ忡 �ߺ��� �ִ��� �˻�
        if (db_exist(name))
        {
            Debug.Log("Name is Exist : " + name);
            return;
        }

        GameData _GameData = new GameData(); // �� ������
        _GameData.name = name;
        _GameData.score = score;

        db_col.InsertOne(_GameData);
    }

    bool db_exist(string name)
    {
        BsonDocument _bson = new BsonDocument { { "name", name } };
        List<GameData> user_list = db_col.Find(_bson).ToList();
        GameData[] user_data = user_list.ToArray();
        if (user_data.Length == 0) return false; // �����Ͱ� ���ٸ� false
        return true;
    }

    void DB_All_View()
    {
        List<GameData> user_list = db_col.Find(user => true).ToList(); // �ݷ����� ����Ʈȭ �Ѵ�.
        GameData[] user_data = user_list.ToArray(); // ����ȭ�� ���� List �ڷ��� -> �迭�� ��ȯ
        for (int i = 0; i < user_data.Length; i++)
        {
            Debug.Log(user_data[i].name + " : " + user_data[i].score);
        }
    }

    void DB_Find(string name)
    {
        // �����͸� �ְ� �޴� ���� json���� ������ ������ ��ȭ�Ѵ�.
        BsonDocument _bson = new BsonDocument { { "name", name } };
        List<GameData> user_list = db_col.Find(_bson).ToList();
        GameData[] user_data = user_list.ToArray();
        for (int i = 0; i < user_data.Length; i++)
        {
            Debug.Log(user_data[i].name + " : " + user_data[i].score);
        }
    }

    void DB_Remove(string name) // �� �ʵ常 ����
    {
        BsonDocument _bson = new BsonDocument { { "name", name } };

        db_col.DeleteOne(_bson);
    }

    void DB_Removes(string name) // ���� name�� �ʵ���� ����
    {
        BsonDocument _bson = new BsonDocument { { "name", name } };

        db_col.DeleteMany(name);
    }

    void DB_Remove_All() // ��ü �ʵ� ����
    {
        BsonDocument _bson = new BsonDocument { }; // <- ��� ����

        db_col.DeleteMany(_bson);
    }

    void DB_Update(string name, int score)
    {
        BsonDocument _bson_search = new BsonDocument { { "name", name } }; // ��ȸ
        BsonDocument _bson_update = new BsonDocument { { "name", name }, { "score", score } }; // Update

        db_col.FindOneAndUpdate(_bson_search, _bson_update);
    }
}