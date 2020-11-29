using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDD.winForm
{
    public partial class WeatherLatestView : Form
    {
        //DBのコネクションを指定
        private readonly string ConnectionString = @"Data Source=C:\Users\hashimototakashi\Documents\C言語\DDD.db;Version=3;";
        public WeatherLatestView()
        {
            InitializeComponent();
        }

        private void LatestButton_Click(object sender, EventArgs e)
        {
            // SQLの作成
            String sql = @"
SELECT DataDate, 
        Condition,
        Temperature
From Weather
WHERE AreaId = @AreaId
ORDER BY DataDate DESC
LIMIT 1
";
            DataTable dt = new DataTable();
            using (var connection = new SQLiteConnection(ConnectionString))
            using (var command = new SQLiteCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@AreaId", this.AreaIdtextBox1.Text);
                using (var adapter = new SQLiteDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
            }

            if (dt.Rows.Count >0)
            {
                DataDateLabel.Text = dt.Rows[0]["DataDate"].ToString();
                ConditionLabel.Text = dt.Rows[0]["Condition"].ToString();
                TemperatureLabel.Text = RoudString(Convert.ToSingle(dt.Rows[0]["Temperature"]), 2) + "℃";
            }
        }
        private  string RoudString(float value, int decimalPoint)
        {
            var temp = Convert.ToSingle(Math.Round(value, decimalPoint));
            return temp.ToString("F" + decimalPoint);
        }
    }
}
