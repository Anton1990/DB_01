using MySql.Data.MySqlClient;

#region подключение к БД
string cs = @"server=localhost;userid=root;password=consys;database=Mytrend";
using var con = new MySqlConnection(cs);
con.Open();
using var cmd = new MySqlCommand();
cmd.Connection = con;
#endregion


MenuShow();


void MenuShow()
{
    System.Console.WriteLine(" ");
    System.Console.WriteLine("Выберите тип операци");
    System.Console.WriteLine("1-удаление таблицы");
    System.Console.WriteLine("2-создание таблицы");
    System.Console.WriteLine("3-вставка элемнотов ");
    System.Console.WriteLine("4-вывод данных");
    System.Console.WriteLine("5-удаление данных");
    System.Console.WriteLine("0-завершить приложение");
    System.Console.WriteLine("------------------------");
    InputChoose();

}

void InputChoose()
{
    string value = Console.ReadLine();
    switch (value.ToLower())
    {
        case "1": Console.WriteLine("Выбрали 1-удаление таблицы"); DelTable(); MenuShow(); break;
        case "2": Console.WriteLine("Выбрали 2-создание таблицы"); CreateTable(); MenuShow(); break;
        case "3": Console.WriteLine("Выбрали 3-вставка элементов"); InsertData(); MenuShow(); break;
        case "4": Console.WriteLine("Выбрали 4-вывод данных"); ShowData(); MenuShow(); break;
        case "5": Console.WriteLine("Выбрали 5-удаление данных");DelData(); MenuShow(); break;
        case "0": Console.WriteLine("Выбрали завершение приложения");  break;
        default: Console.WriteLine("Выбрали Дефолт"); MenuShow(); break;
    }

}

#region Операции с базой данных

void DelTable()
{

    cmd.CommandText = "DROP TABLE IF EXISTS cars";
    cmd.ExecuteNonQuery();

}
void CreateTable()
{
    try
    {
        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS cars(id INTEGER PRIMARY KEY AUTO_INCREMENT,
        name TEXT, price INT)";
        cmd.ExecuteNonQuery();
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}
void InsertData()
{
    try
    {
        CreateTable();
        Console.WriteLine("Введите Model");
        string model = Console.ReadLine();

        Console.WriteLine("Введите цену");
        int number = Convert.ToInt32(Console.ReadLine());

        cmd.CommandText = $"INSERT INTO cars(name, price) VALUES('{model}','{number}') ";
        cmd.ExecuteNonQuery();
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
  


}
void ShowData()
{
    try
    {
        string sql = "SELECT * FROM cars WHERE ID>0";
        using var cmdout = new MySqlCommand(sql, con);
        using MySqlDataReader rdr = cmdout.ExecuteReader();

        while (rdr.Read())
        {
            Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                    rdr.GetInt32(2));
        }
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}
void DelData()
{
    try
    {
        string sql = " DELETE from cars WHERE ID>0";
        using var cmd = new MySqlCommand(sql, con);
        using MySqlDataReader rdr = cmd.ExecuteReader();
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }

}

#endregion




