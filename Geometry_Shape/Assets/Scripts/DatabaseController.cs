using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;

public class DatabaseController : MonoBehaviour
{
    //Referencias
    public PauseMenuController pauseMenuController;
    public PlayerController playerController;
    public int[] sceneLoaded = new int[3];
    private int count = -1;
    public static int slotPressedDatabase;


    //Estadísticas
    public static int jumps;
    public static int time;

    private void Start()
    {
        sceneLoaded = new int[3];
    }

    //Para leer la base de datos
    public void ReadStatistics()
    {
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/DatabaseGS.db"; //Ruta para la base de datos.
        
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Se abre la conxión con la base de datos.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT name_stat, quantity_stat FROM statistics";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            string nameStat = reader.GetString(0);
            int quantityStat = reader.GetInt32(1);

            if (nameStat.Equals("number_jumps"))
            {
                jumps = quantityStat;
            }
            else if(nameStat.Equals("total_time"))
            {
                time = quantityStat;
            }
        }

        pauseMenuController.StatisticsMenu(jumps, time);

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    //Actualiza la base de datos
    public void UpdateStatistics()
    {
        UpdateJumps(); //Actualiza los saltos
        UpdateTime(); //Actualiza el tiempo total
    }

    public void UpdateJumps()
    {
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/DatabaseGS.db"; //Ruta para la base de datos.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Se abre la conxión con la base de datos.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQueryJump = "UPDATE statistics SET quantity_stat = " + playerController.nJump + " WHERE name_stat = 'number_jumps'";
        dbcmd.CommandText = sqlQueryJump;

        IDataReader reader = dbcmd.ExecuteReader();

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void UpdateTime()
    {
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/DatabaseGS.db"; //Ruta para la base de datos.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Se abre la conxión con la base de datos.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQueryTime = "UPDATE statistics SET quantity_stat = " + playerController.totalTime + " WHERE name_stat = 'total_time'";
        dbcmd.CommandText = sqlQueryTime;

        IDataReader reader = dbcmd.ExecuteReader();

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    //Se lee de la Base de datos el punto de guardado de cada hueco de partidas guardadas.
    public int[] GetLoadScene()
    {
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/DatabaseGS.db"; //Ruta para la base de datos.

        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Se abre la conxión con la base de datos.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQuery = "SELECT id, scene_to_load FROM games";
        dbcmd.CommandText = sqlQuery;
        IDataReader reader = dbcmd.ExecuteReader();

        while (reader.Read())
        {
            count += 1;
            int id = reader.GetInt32(0);
            int sceneToLoad = reader.GetInt32(1);

            if (sceneToLoad != -1)
            {
                sceneLoaded[count] = sceneToLoad;
            }
            else
            {
                sceneLoaded[count] = -1;
            }

        }

        count = -1;

        if (pauseMenuController != null)
        {
            pauseMenuController.StatisticsMenu(jumps, time);
        }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;

        return sceneLoaded;
    }

    public void SetLoadScene(int sceneToSet, int slotPressed)
    {
        slotPressedDatabase = slotPressed;
        string conn = "URI=file:" + Application.dataPath + "/StreamingAssets/DatabaseGS.db"; //Ruta para la base de datos.
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); //Se abre la conxión con la base de datos.
        IDbCommand dbcmd = dbconn.CreateCommand();
        string sqlQueryScene = "UPDATE games SET scene_to_load = " + sceneToSet + " WHERE id = " + slotPressed;
        dbcmd.CommandText = sqlQueryScene;

        IDataReader reader = dbcmd.ExecuteReader();

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
