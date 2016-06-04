﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Phidgets;
using Phidgets.Events;

namespace ShopApp
{
    //dont forget to make an if for selling more then you have!
    public partial class Form1 : Form
    {
        List<Product> Shop;
        DBHelper dbh;
        private RFID myRFIDReader;
        int cocacolas;
        int fantas;
        int sprites;
        int hotdogs;
        int pizzas;
        int burgers;
        int fries;
        int beers;
        int women;

        int cocacolasInBasket;
        int fantasInBasket;
        int spritesInBasket;
        int hotdogsInBasket;
        int pizzasInBasket;
        int burgersInBasket;
        int friesInBasket;
        int beersInBasket;
        int womenInBasket;

        int quantity;
        double balance;
        string tag;
        int workingRFID;

        double totalPrice;

        Stack<Product> basket;
        Stack<Product> reverseBasket;
        public Form1()
        {
            InitializeComponent();
            workingRFID = 0;
            dbh = new DBHelper();
            try
            {
                myRFIDReader = new RFID();
                openRFID();
                myRFIDReader.Tag += new TagEventHandler(ProcessThisTag);
                
            }
            catch (PhidgetException) { listBox1.Items.Add("error at start-up."); }

            cocacolas = dbh.GetQuantity("CocaCola");
            fantas = dbh.GetQuantity("Fanta");
            sprites = dbh.GetQuantity("Sprite");
            hotdogs = dbh.GetQuantity("HotDog");
            pizzas = dbh.GetQuantity("Pizza");
            burgers = dbh.GetQuantity("Burger");
            fries = dbh.GetQuantity("Fries");
            beers = dbh.GetQuantity("Beer");
            women = dbh.GetQuantity("SexyLady");

            cocacolasInBasket = 0;
            fantasInBasket = 0;
            spritesInBasket = 0;
            hotdogsInBasket = 0;
            pizzasInBasket = 0;
            burgersInBasket = 0;
            friesInBasket = 0;
            beersInBasket = 0;
            womenInBasket = 0;

            tbPrice.Text = "0";
            balance = 0;
            quantity = 1;
            basket = new Stack<Product>();
            reverseBasket = new Stack<Product>();
            Shop = dbh.GetAllProducts();
            foreach(Product p in Shop)
            {

            }

        }

        private void openRFID()
        {
            try
            {
                myRFIDReader.open();
                myRFIDReader.waitForAttachment(3000);
                myRFIDReader.Antenna = true;
                myRFIDReader.LED = true;
            }
            catch (PhidgetException) { MessageBox.Show("no RFID-reader opened"); }
        }

        private void ProcessThisTag(object sender, TagEventArgs e)
        {
            int paid = dbh.CheckIfPaid(e.Tag);
            if (paid == 1 && workingRFID==0 )
            {

                btnScan.Visible = false;
                balance = dbh.getBalance(e.Tag);
                tbBalance.Text = Convert.ToString(balance);
                tag = e.Tag;
                workingRFID = 1;

            }
            else
            {
                MessageBox.Show("Invalid RFID / RFID Already in use");
            }
        }
        private void btnPurchase_Click(object sender, EventArgs e)
        {
            if (balance > totalPrice)
            {
                double newbalance = balance-totalPrice;
                dbh.UpdateBalance(tag, newbalance);

                int newcocacolas = cocacolas - cocacolasInBasket;
                dbh.UpdateQuantity("CocaCola", newcocacolas);

                int newfantas = fantas - fantasInBasket;
                dbh.UpdateQuantity("Fanta", newfantas);

                int newsprites = sprites - spritesInBasket;
                dbh.UpdateQuantity("Sprite", newsprites);

                int newhotdogs = hotdogs - hotdogsInBasket;
                dbh.UpdateQuantity("HotDog", newhotdogs);

                int newpizzas = pizzas - pizzasInBasket;
                dbh.UpdateQuantity("Pizza", newpizzas);

                int newburgers = burgers - burgersInBasket;
                dbh.UpdateQuantity("Burger", newburgers);

                int newfries = fries - friesInBasket;
                dbh.UpdateQuantity("Fries", newfries);

                int newbeers = beers - beersInBasket;
                dbh.UpdateQuantity("Beer", newbeers);

                int newwomen = women - womenInBasket;
                dbh.UpdateQuantity("SexyLady", newwomen);


                listBox1.Items.Clear();

                cocacolasInBasket = 0;
                fantasInBasket = 0;
                spritesInBasket = 0;
                hotdogsInBasket = 0;
                pizzasInBasket = 0;
                burgersInBasket = 0;
                friesInBasket = 0;
                beersInBasket = 0;
                womenInBasket = 0;

                balance = 0;
                totalPrice = 0;
                tbPrice.Text = totalPrice.ToString();
                workingRFID = 0;
                


                MessageBox.Show("Purchase successfull");
                btnScan.Visible = true;
            }
            else { MessageBox.Show("Not enough money"); }
        }

        int i = 0;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //for showing items in one row like: CocaCola 5
            int clickproducts = 0;

            while (quantity > i)
            {
                if (cocacolasInBasket < cocacolas)
                {
                    cocacolasInBasket++;
                    //listBox1.Items.Add("CocaCola");
                    totalPrice += 2.5;
                    tbPrice.Text = totalPrice.ToString();
                    i++;
                    clickproducts++;
                }
                else
                {
                    MessageBox.Show("Not enough items in stock");
                    break;
                }
            }
            if (clickproducts > 0)
            {
                listBox1.Items.Add("CocaCola " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("CocaCola", 2.5, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int clickproducts = 0;
            while (quantity > i)
            {
                if (fantasInBasket < fantas)
                {
                    fantasInBasket++;
                   // listBox1.Items.Add("Fanta");
                    totalPrice += 2.5;
                    tbPrice.Text = totalPrice.ToString();
                    i++;
                    clickproducts++;
                }
                else { MessageBox.Show("Not enough items in stock");
                    break;
                }
            }
            if (clickproducts > 0)
            {
                listBox1.Items.Add("Fanta " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("Fanta", 2.5, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int clickproducts = 0;
            while (quantity > i)
            {
                if (spritesInBasket < sprites)
                {
                    spritesInBasket++;
                    //listBox1.Items.Add("Sprite");
                    totalPrice += 2.5;
                    tbPrice.Text = totalPrice.ToString();
                    i++;
                    clickproducts++;
                }
                else { MessageBox.Show("Not enough items in stock");
                    break;
                }
                
            }
            if (clickproducts > 0)
            {
                listBox1.Items.Add("Sprite " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("Sprite", 2.5, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            int clickproducts = 0;
            while (quantity > i)
            {
                if (hotdogsInBasket < hotdogs)
                {
                    hotdogsInBasket++;
                //listBox1.Items.Add("HotDog");
                totalPrice += 3;
                tbPrice.Text = totalPrice.ToString();
                i++;
                    clickproducts++;
                }
                else { MessageBox.Show("Not enough items in stock");
                    break;
                }
            }
            if (clickproducts > 0)
            {
                listBox1.Items.Add("HotDog " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("HotDog", 3, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int clickproducts = 0;
            while (quantity > i)
            {
                if (pizzasInBasket < pizzas)
                {
                    pizzasInBasket++;
                //listBox1.Items.Add("Pizza");
                totalPrice += 3.5;
                tbPrice.Text = totalPrice.ToString();
                i++;
                    clickproducts++;
                }
                else { MessageBox.Show("Not enough items in stock");
                    break;
                }
            }
            if (clickproducts > 0)
            {
                listBox1.Items.Add("Pizza " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("Pizza", 3.5, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int clickproducts = 0;
            while (quantity > i)
            {
                if (burgersInBasket < burgers)
                {
                    burgersInBasket++;
                //listBox1.Items.Add("Burger");
                totalPrice += 4;
                tbPrice.Text = totalPrice.ToString();
                i++;
                    clickproducts++;
                }
                else { MessageBox.Show("Not enough items in stock");
                    break;
                }
            }
            if (clickproducts > 0)
            {
                listBox1.Items.Add("Burger " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("Burger", 4, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            int clickproducts = 0;
            while (quantity > i)
            {
                if (friesInBasket < fries)
                {
                    friesInBasket++;
               // listBox1.Items.Add("Fries");
                totalPrice += 3;
                tbPrice.Text = totalPrice.ToString();
                i++;
                    clickproducts++;
                }
                else { MessageBox.Show("Not enough items in stock");
                    break;
                }
            }
            if (clickproducts > 0)
            {
                listBox1.Items.Add("Fries " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("Fries", 3, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            int clickproducts = 0;
            while (quantity > i)
            {
                if (beersInBasket < beers)
                {
                    beersInBasket++;
                //listBox1.Items.Add("Beer");
                totalPrice += 3;
                tbPrice.Text = totalPrice.ToString();
                i++;
                    clickproducts++;
                }
                else { MessageBox.Show("Not enough items in stock");
                    break;
                }
            }
            if (clickproducts > 0)
            {
                listBox1.Items.Add("Beer " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("Beer", 3, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            int clickproducts = 0;
            while (quantity > i)
            {
                if (womenInBasket < women)
                {
                    womenInBasket++;
                //listBox1.Items.Add("SexyLady");
                totalPrice += 2.5;
                tbPrice.Text = totalPrice.ToString();
                i++;
                clickproducts++;
                }
                else { MessageBox.Show("Not enough items in stock");
                    break;
                }
            }
            if(clickproducts>0)
            {
                listBox1.Items.Add("SexyLady " + clickproducts.ToString());
                //for the undo button
                Product temp = new Product("SexyLady", 2.5, clickproducts);
                reverseBasket.Clear();
                basket.Push(temp);
            }
            i = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            cocacolasInBasket = 0;
            fantasInBasket = 0;
            spritesInBasket = 0;
            hotdogsInBasket = 0;
            pizzasInBasket = 0;
            burgersInBasket = 0;
            friesInBasket = 0;
            beersInBasket = 0;
            womenInBasket = 0;

            totalPrice = 0;
            tbPrice.Text = totalPrice.ToString();
        }

        private void tbQuantity_TextChanged(object sender, EventArgs e)
        {
            try {
                quantity = Convert.ToInt32(tbQuantity.Text);
            }
            catch
            {
                MessageBox.Show("Not a valid value");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //my mistake
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            myRFIDReader.LED = false;
            myRFIDReader.Antenna = false;
            myRFIDReader.close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            cocacolasInBasket = 0;
            fantasInBasket = 0;
            spritesInBasket = 0;
            hotdogsInBasket = 0;
            pizzasInBasket = 0;
            burgersInBasket = 0;
            friesInBasket = 0;
            beersInBasket = 0;
            womenInBasket = 0;

            totalPrice = 0;
            tbPrice.Text = totalPrice.ToString();
            balance = 0;
            btnScan.Visible = true;
            workingRFID = 0;
            basket.Clear();
        }

        Product temp;
        private void button1_Click(object sender, EventArgs e)
        {
            if (basket.Count > 0)
            {
                temp = basket.Pop();
                reverseBasket.Push(temp);
                totalPrice = totalPrice - (temp.Price * temp.Quantity);
                tbPrice.Text = totalPrice.ToString();

                switch (temp.Name)
                {
                    case "CocaCola":
                        cocacolasInBasket = cocacolasInBasket - temp.Quantity;
                        break;
                    case "Fanta":
                        fantasInBasket = fantasInBasket - temp.Quantity;
                        break;
                    case "Sprite":
                        spritesInBasket = spritesInBasket - temp.Quantity;
                        break;
                    case "HotDog":
                        hotdogsInBasket = hotdogsInBasket - temp.Quantity;
                        break;
                    case "Pizza":
                        pizzasInBasket = pizzasInBasket - temp.Quantity;
                        break;
                    case "Burger":
                        burgersInBasket = burgersInBasket - temp.Quantity;
                        break;
                    case "Fries":
                        friesInBasket = friesInBasket - temp.Quantity;
                        break;
                    case "Beer":
                        beersInBasket = beersInBasket - temp.Quantity;
                        break;
                    case "SexyLady":
                        womenInBasket = womenInBasket - temp.Quantity;
                        break;
                }
                listBox1.Items.RemoveAt(listBox1.Items.Count - 1);
            }
            else { MessageBox.Show("Nothing to undo"); }
        }



        /// <summary>
        /// Generates a panel with all the controls inside. 
        /// X and Y are passed as reference and are updated during the runtime of the method
        /// </summary>
        /// <param name="Prod"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        private void GenerateProductControl(Product Prod, ref int X, ref int Y)
        {
            // margin for the panel
            const int LEFTMARGIN = 9, TOPMARGIN = 3;
            // position of a button inside of a panel. always stays unchanged
            const int BTNX = 6, BTNY = 26;
            // position of a label sotring the product name
            const int LBLPRODUCTNAMEX = 3, LBLPRODUCTNAMEY = 8;
            // position of a unit storing the price and a currency
            const int LBLUNITPRICEX = 87, LBLLEVELTWOY = 173, LBLCURRENCYX = 128;
            // the length and the width of a button
            const int BTNSIDE = 140; // the other labels should be positioned this length away from the top border
            // the size of a panel
            const int PANELWIDTH = 152, PANELHEIGHT = 224;
            // the position of the queantity labels
            const int LBLQUANX = 3, LBLLEVELTHREE = 201, LBLQUANVALX = 124;
            // each panel has 5 labels and 1 button
            Panel myPanel;
            Button myButton;
            Label myLblProductName;
            Label myLblProductPrice;
            Label myLblCurrency;
            Label myLblProductQuantityCaption;
            Label myLblProductQuantity;


            //if you need them you may store them in an array, otherwise do it anonymously

            //int i = 0;
            //foreach (DeliciousStuff ds in this.myShop.getMyDeliciousStuff())
            //{
            // panel
            myPanel = new Panel();
            myPanel.Location = new System.Drawing.Point(X, Y);
            myPanel.Size = new System.Drawing.Size(PANELWIDTH, PANELHEIGHT);
            //myPanel.Name = "pnlTest";
            myPanel.BackColor = Color.AliceBlue;

            // Product Name
            myLblProductName = new Label();
            myLblProductName.Location = new System.Drawing.Point(LBLPRODUCTNAMEX, LBLPRODUCTNAMEY);
            myLblProductName.Text = Prod.Name;

            // Product price
            myLblProductPrice = new Label();
            myLblProductPrice.Location = new System.Drawing.Point(LBLUNITPRICEX, LBLLEVELTWOY);
            myLblProductPrice.Text = Prod.Price.ToString();

            // Currency
            myLblCurrency = new Label();
            myLblCurrency.Text = "c";
            myLblProductPrice.Location = new System.Drawing.Point(LBLCURRENCYX, LBLLEVELTWOY);

            // Quantity caption
            myLblProductQuantityCaption = new Label();
            myLblProductQuantityCaption.Text = "Quantity: ";
            myLblProductQuantityCaption.Location = new System.Drawing.Point(LBLQUANX, LBLLEVELTHREE);
            
            // tha actual prison
            myLblProductQuantity = new Label();
            myLblProductQuantity.Text = Prod.Quantity.ToString();
            myLblProductQuantityCaption.Location = new System.Drawing.Point(LBLQUANVALX, LBLLEVELTHREE);

            myButton = new Button();
            myButton.Location = new System.Drawing.Point(BTNX, BTNY);
            myButton.Size = new System.Drawing.Size(BTNSIDE, BTNSIDE);
            myButton.Text = Prod.Name;
            myButton.Click += new EventHandler(this.SellShit);
            myButton.Tag = Prod;
            i++;
            myPanel.Controls.Add(myButton);
            myPanel.Controls.Add(myLblProductName);
            myPanel.Controls.Add(myLblProductPrice);
            myPanel.Controls.Add(myLblCurrency);
            myPanel.Controls.Add(myLblProductQuantityCaption);
            myPanel.Controls.Add(myLblProductQuantity);
            this.Controls.Add(myPanel);
            
            X = X + PANELWIDTH + LEFTMARGIN;
            if(X>= this.pnlProducts.Width + LEFTMARGIN)
            {
                X = 9;
                Y = Y + TOPMARGIN + PANELHEIGHT;
            }
        }

        private void SellShit(object sender, EventArgs e)
        {

        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (reverseBasket.Count > 0)
            {
                temp = reverseBasket.Pop();
                basket.Push(temp);
                totalPrice = totalPrice + (temp.Price * temp.Quantity);
                tbPrice.Text = totalPrice.ToString();
                switch (temp.Name)
                {
                    case "CocaCola":
                        cocacolasInBasket = cocacolasInBasket + temp.Quantity;
                        break;
                    case "Fanta":
                        fantasInBasket = fantasInBasket + temp.Quantity;
                        break;
                    case "Sprite":
                        spritesInBasket = spritesInBasket + temp.Quantity;
                        break;
                    case "HotDog":
                        hotdogsInBasket = hotdogsInBasket + temp.Quantity;
                        break;
                    case "Pizza":
                        pizzasInBasket = pizzasInBasket + temp.Quantity;
                        break;
                    case "Burger":
                        burgersInBasket = burgersInBasket + temp.Quantity;
                        break;
                    case "Fries":
                        friesInBasket = friesInBasket + temp.Quantity;
                        break;
                    case "Beer":
                        beersInBasket = beersInBasket + temp.Quantity;
                        break;
                    case "SexyLady":
                        womenInBasket = womenInBasket + temp.Quantity;
                        break;
                }
                listBox1.Items.Add(temp.Name + " " + temp.Quantity.ToString());
            }
            else { MessageBox.Show("Nothing to redo"); }
        }
    }
}
