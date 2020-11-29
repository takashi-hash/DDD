using DDD.winForm.Common;
using DDD.winForm.Data;
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
        
        public WeatherLatestView()
        {
            InitializeComponent();
        }

        private void LatestButton_Click(object sender, EventArgs e)
        {
            // SQLの作成

            var dt = WeatherSqLite.GetLatest(Convert.ToInt32(AreaIdtextBox1.Text));
            if (dt.Rows.Count >0)
            {
                DataDateLabel.Text = dt.Rows[0]["DataDate"].ToString();
                ConditionLabel.Text = dt.Rows[0]["Condition"].ToString();
                TemperatureLabel.Text = CommonFunc.RoudString(
                    Convert.ToSingle(dt.Rows[0]["Temperature"])
                    , CommonConst.TemperatureDecimalPoint) + CommonConst.TemperatureUnitName;
            }
        }
    }
}
