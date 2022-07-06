using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace DB_Controller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public List<Item> dataBase;
        DataController dc;

        private void Form1_Load(object sender, EventArgs e)
        {
            dataBase = new List<Item>();
            dc = new DataController();
            dc.loadItems(dataBase);
            addToDGV1(dataBase);
        }

        //Adds new item to the list
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int id = dataBase.Count + 1;
            string itemName = "";
            int price;
            int quantity;
            bool correct = true;

            if (tbItemName.Text == "")
            {
                MessageBox.Show("Enter the item name");
                correct = false;
            }
            else if (itemNameAlreadyUsed(tbItemName.Text))
            {
                MessageBox.Show("An item with a selected name already exists");
                correct = false;
            }
            else
            {
                itemName = tbItemName.Text;
            }

            if (tbPrice.Text == "")
            {
                price = 0;
            }
            else
            {
                try
                {
                    price = Convert.ToInt32(tbPrice.Text);
                }
                catch
                {
                    MessageBox.Show("Enter a number for price");
                    price = 0;
                    correct = false;
                }
            }

            if (tbQuantity.Text == "")
            {
                quantity = 0;
            }
            else
            {
                try
                {
                    quantity = Convert.ToInt32(tbQuantity.Text);
                }
                catch
                {
                    MessageBox.Show("Enter a number for quantity");
                    quantity = 0;
                    correct = false;
                }
            }

            if (correct)
            {
                dataBase.Add(new Item(id, itemName, price, quantity));
                refreshDGV();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dc.clearDB();
            dc.saveToDB(dataBase);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int selectedRowCount = dataGridView1.CurrentCell.RowIndex;
            int id = Convert.ToInt32(dataGridView1[0, selectedRowCount].Value.ToString());
            Debug.WriteLine("Row: " + selectedRowCount + "   ID: " + id);

            dataBase.RemoveAt(id - 1);
            sortIDs();
            refreshDGV();
        }

        #region Methods

        private void addToDGV1(List<Item> dataBase)
        {
            for (int i = 0; i < dataBase.Count; i++)
            {
                addToDGV2(dataBase[i].id, dataBase[i].name, dataBase[i].price, dataBase[i].quantity);
            }
        }

        private void addToDGV2(int id, string name, double price, int quantity)
        {
            string[] row = { Convert.ToString(id), name, Convert.ToString(price), Convert.ToString(quantity) };
            dataGridView1.Rows.Add(row);
        }

        private bool itemNameAlreadyUsed(string itemName)
        {
            foreach (Item item in dataBase)
            {
                if (item.name == itemName)
                {
                    return true;
                }
            }
            return false;
        }

        private void refreshDGV()
        {
            dataGridView1.Rows.Clear();
            addToDGV1(dataBase);
        }

        private void sortIDs()
        {
            int i = 1;
            foreach (Item item in dataBase)
            {
                item.id = i;
                i++;
            }
        }
        #endregion
    }
}
