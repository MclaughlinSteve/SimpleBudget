using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SPFA
{
    /// <summary>
    /// Interaction logic for Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        private List<MoneyData> expenses;
        private List<MoneyData> income;

        /*
         * Constructor for graph window
         * Goes through the values in both the income and expenses lists associated with the current user
         * plots datapoints on two separate graphs depending on which list they come from
         * 
         * May want to update the X axis in the future. Currently the x axis doesn't really make much sense to a lay person
         * but the income x-axis is used for pay periods. The x-axis for expenses should just sync up with the income axis.
         * The expenses x-axis is number of expenditures during the number of pay periods listed in the income graph x-axis
         * 
         * */
        public Graph(List<MoneyData> expenses, List<MoneyData> income)
        {
            this.income = income;
            this.expenses = expenses;
            InitializeComponent();

            if (expenses.Count > 0)
            {
                    double Xmax = 0; // adjusted max values
                    double Ymax = 0;
                    LineCharts.DataSeries ds = new LineCharts.DataSeries();
                    ds.LineColor = Brushes.Orange;
                    ds.LineThickness = 1;
                    ds.SeriesName = "Expenses";
                    ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Circle;
                    Expenses.OriginalData.DataList.Add(ds);
                    //Graph expenses on expenses graph
                    for (int i = 0; i < expenses.Count; i++)
                    {
                        double x = i;
                        double y = expenses[i].Amount;
                        if (Xmax < x) Xmax = x;
                        if (Ymax < y) Ymax = y;

                        ds.LineSeries.Points.Add(new Point(x, y));
                    }
                    Expenses.Ymax = Ymax * 1.1;
                    Expenses.Ymin = 0;
                    Expenses.YTick = (Expenses.Ymax - Expenses.Ymin) / 10;
                    Expenses.Xmin = 0;
                    Expenses.Xmax = Xmax;
                    Expenses.XTick = (Expenses.Xmax - Expenses.Xmin) / 10;


 
                Expenses.Refresh(); //Refreshes the graph to show changes
            }
            if (income.Count > 0)
            {
                double Xmax = 0; // adjusted max values
                double Ymax = 0;
                LineCharts.DataSeries ds = new LineCharts.DataSeries();
                ds.LineColor = Brushes.Orange;
                ds.LineThickness = 1;
                ds.SeriesName = "Income";
                ds.Symbols.SymbolType = LineCharts.Symbols.SymbolTypeEnum.Circle;
                Income.OriginalData.DataList.Add(ds);
                for (int i = 0; i < income.Count; i++)
                {
                    double x = i;
                    double y = income[i].Amount;
                    if (Xmax < x) Xmax = x;
                    if (Ymax < y) Ymax = y;
                    ds.LineSeries.Points.Add(new Point(x, y));
                }
                Income.Ymax = Ymax*1.1;
                Income.Ymin = 0;
                Income.YTick = (Expenses.Ymax - Income.Ymin) / 10;
                Income.Xmin = 0;
                Income.Xmax = Xmax;
                Income.XTick = (Expenses.Xmax - Income.Xmin) / 10;
                //Graph income on income graph
                Income.Refresh(); //Refreshes the graph to show changes
            }
        }
        
        //Closes the current window
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
