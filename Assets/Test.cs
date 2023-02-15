using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using System.Data;

public class Test : MonoBehaviour
{
    public static MySqlConnection conn;

    static string db_ip = "43.201.56.186";
    static string db_port = "3306";
    static string db_name = "unity";
    static string db_id = "userdata";
    static string db_pw = "1207";



    string strConn;
    // Start is called before the first frame update
    private void Awake()
    {
        strConn = string.Format("Server={0};Database={1};Uid={2};Pwd={3};", db_ip, db_name, db_id, db_pw);
        
        Debug.Log(strConn);

        try
        {
            conn = new MySqlConnection(strConn);
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log("접속실패");
        }
    }

    private void Start()
    {
        string query = "select  * from user";

        DataSet ds = Print(query);
        Debug.Log(ds.GetXml());
    }



    private DataSet Print(string query)
    {
        try
        {
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = query;

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sd.Fill(ds, "user");

            conn.Close();
            return ds;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
            Debug.Log("실패");

            return null;
        }

    }


}
