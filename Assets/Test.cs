using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using System.Data;
using UnityEngine.UI;
using System.Xml;

public class Test : MonoBehaviour
{
    public InputField inputId;
    public InputField inputPw;

    public static MySqlConnection conn;

    static string db_ip = "3.35.254.185";
    static string db_port = "3306";
    static string db_name = "userdata";
    static string db_id = "user";
    static string db_pw = "1207";

    static string qExistsId = "";
    static string qExistsId1 = "SELECT * FROM user WHERE id = ";
    


    // static string qExistsPw = "";
    // static string qExistsPw1 = "SELECT EXISTS(SELECT * FROM user WHERE pw = '";
    // static string qExistsPw2 = "')as success;";

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
        }
    }

    private void Start()
    {
        string query = "select * from user;";
        // id = bowon
        // query +
        DataSet ds = Print(query);
        

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
            Debug.Log(ds.GetXml());
            ds.WriteXml("dataset.xml");
            conn.Close();
            return ds;
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());

            return null;
        }

    }

    public void Button1(){
        string id = inputId.text; 
        string pw = inputPw.text; 
        qExistsId = qExistsId1 + "'" + id + "';";
        DataSet dsId = Print(qExistsId);
        //dsId.WriteXml("dataset.xml");
        


        
    }



}
/*
string connectionString = string.Format("Provider=System.Data.Sqlite;Data Source={0};Version=3;", "sqlite.db");
// 데이터베이스에서 테이블을 추출하는 Parser 클래스의 인스턴스를 만듭니다.
// 연결 문자열은 첫 번째 매개변수로 전달되고 LoadOptions는 데이터베이스 파일 형식으로 설정됩니다.
using (Parser parser = new Parser(connectionString, new LoadOptions(FileFormat.Database)))
{
	// 테이블 목록 가져오기
	IEnumerable<TocItem> toc = parser.GetToc();
	// 테이블에 대해 반복
	foreach (TocItem i in toc)
	{
		// 테이블 이름 인쇄
		Console.WriteLine(i.Text);
		// 테이블 내용을 텍스트로 추출
		using (TextReader reader = parser.GetText(i.PageIndex.Value))
		{
			Console.WriteLine(reader.ReadToEnd());
		}
	}
}
*/