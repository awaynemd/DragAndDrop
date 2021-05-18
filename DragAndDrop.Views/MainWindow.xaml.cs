using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragAndDrop.Views
{
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void listBox1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            object item = listBox1.SelectedItem;
            if (item != null)
                DragDrop.DoDragDrop(listBox1, item, DragDropEffects.Move);
        }

        private void grid1_Drop(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = e.Source as TextBlock;

            DataObject item = (((DragEventArgs)e).Data) as DataObject;

            // This next line fails, listItem is null!   
            ListBoxItem listItem = item.GetData(typeof(ListBoxItem)) as ListBoxItem;
            textBlock.Text = listItem.Content.ToString();
        }

    }
}