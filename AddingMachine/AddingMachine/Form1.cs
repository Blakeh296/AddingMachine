using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddingMachine
{
    public partial class AddingMachine : Form
    {
        double[] tapeArray = new double[50];
        int i = 0;
        double sum = 0;

        public AddingMachine()
        {
            InitializeComponent();
        }

        private int AddToArray(double NewValue)
        {
            int returnValue = -1;  // Index of new array element.

            try
            {
                // Iterate through array looking for a zero value to replace.
                for (int x = 0; x <= tapeArray.GetUpperBound(0); x++)
                {
                    if (tapeArray[x] == 0)
                    {
                        // When an empty element is found, insert the
                        // new value and get the element. 
                        tapeArray[x] = NewValue;
                        returnValue = x;
                        break; // Exit loop.
                    }
                }

                // If no empty elements were found, roll over the array
                // and insert the new value in the second element.
                if (returnValue == -1)
                {
                    RolloverArray();
                    tapeArray[1] = NewValue;
                    returnValue = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returnValue;
        }

        private void RolloverArray()
        {
            double arraySum = 0;

            // Get the sum of all elements in the array, clear the
            // array and then carry over the sum to the first element.
            arraySum = tapeArray.Sum();
            Array.Clear(tapeArray, 0, tapeArray.GetUpperBound(0) + 1);
            tapeArray[0] = arraySum;
        }


        private void WaitForNewEntry()
        {
            // Scroll to bottom of listbox and reset txtEntry.
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            textBox1.Clear();
            textBox1.Focus();
        }


       

        private void button1_Click(object sender, EventArgs e)
        {

            double valueEntered = 0;

            try
            {
                if (comboBox1.Text == "ADD" && double.TryParse(textBox1.Text, out valueEntered))
                {
                    listBox1.Items.Add("+ " + textBox1.Text);
                    AddToArray(valueEntered);
                    WaitForNewEntry();
                    DisplayTotal();
                    label2.ForeColor = System.Drawing.Color.Green;
                    label2.Text = "New Total: " + tapeArray.Sum().ToString();
                }
                else if (comboBox1.Text == "SUBTRACT" && double.TryParse(textBox1.Text, out valueEntered))
                {
                    listBox1.Items.Add("- " + textBox1.Text);
                    AddToArray(Math.Abs(valueEntered) * -1);
                    WaitForNewEntry();
                    DisplayTotal();
                    label2.ForeColor = System.Drawing.Color.Green;
                    label2.Text = "New Total: " + tapeArray.Sum().ToString();
                }
                else if (comboBox1.Text == "MULTIPLY" && double.TryParse(textBox1.Text, out valueEntered))
                {
                    RolloverArray();
                    DisplayTotal();
                    listBox1.Items.Add("x " + valueEntered);
                    tapeArray[0] *= valueEntered;
                    DisplayTotal();
                    WaitForNewEntry();
                    label2.ForeColor = System.Drawing.Color.Green;
                    label2.Text = "History";
                }
                else if (comboBox1.Text == "DIVIDE" && double.TryParse(textBox1.Text, out valueEntered))
                {
                    RolloverArray();
                    DisplayTotal();
                    listBox1.Items.Add("/ " + valueEntered);
                    tapeArray[0] /= valueEntered;
                    DisplayTotal();
                    WaitForNewEntry();
                    label2.ForeColor = System.Drawing.Color.Green;
                    label2.Text = "History";
                }
                else
                {
                    MessageBox.Show("Enter a number first..");
                }

            }
            catch
            {
                MessageBox.Show("Error..");
            }

        }


        private void DisplayTotal()
        {
            // Display the current total with lines and whitespace.
            listBox1.Items.Add("-------------------------------");
            listBox1.Items.Add("New Total: " + tapeArray.Sum().ToString());
            listBox1.Items.Add("-------------------------------");
        }

        private void label3_MouseHover(object sender, EventArgs e)
        {
            listBox1.Items.Add("-------------------------------");
            listBox1.Items.Add("DOUBLE CLICK TO CLEAR ALL");
            listBox1.Items.Add("-------------------------------");
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                label2.ForeColor = System.Drawing.Color.Black;
                label2.Text = "History";
                // Clear everything and return to the entry textbox.
                Array.Clear(tapeArray, 0, tapeArray.GetUpperBound(0) + 1);
                listBox1.Items.Clear();
                WaitForNewEntry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error ...");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                double valueEntered = 0;

                if (e.KeyData == Keys.Enter)
                {
                    if (comboBox1.Text == "ADD" && double.TryParse(textBox1.Text, out valueEntered))
                    {
                        label2.ForeColor = System.Drawing.Color.Black;
                        label2.Text = "History: " + "+ " + valueEntered.ToString();
                        listBox1.Items.Add("+ " + textBox1.Text);
                        AddToArray(valueEntered);
                        WaitForNewEntry();
                        textBox1.Text = "0";
                    }
                    else if (comboBox1.Text == "SUBTRACT" && double.TryParse(textBox1.Text, out valueEntered))
                    {
                        label2.ForeColor = System.Drawing.Color.Black;
                        label2.Text = "History: " + "- " + valueEntered.ToString();
                        listBox1.Items.Add("- " + textBox1.Text);
                        AddToArray(Math.Abs(valueEntered) * -1);
                        WaitForNewEntry();
                        textBox1.Text = "0";
                    }
                    else if (comboBox1.Text == "MULTIPLY" && double.TryParse(textBox1.Text, out valueEntered))
                    {
                        label2.ForeColor = System.Drawing.Color.Green;
                        label2.Text = "History";
                        RolloverArray();
                        DisplayTotal();
                        listBox1.Items.Add("x " + valueEntered);
                        tapeArray[0] *= valueEntered;
                        DisplayTotal();
                        WaitForNewEntry();
                        textBox1.Text = "0";
                    }
                    else if (comboBox1.Text == "DIVIDE" && double.TryParse(textBox1.Text, out valueEntered))
                    {
                        label2.ForeColor = System.Drawing.Color.Green;
                        label2.Text = "History";
                        RolloverArray();
                        DisplayTotal();
                        listBox1.Items.Add("/ " + valueEntered);
                        tapeArray[0] /= valueEntered;
                        DisplayTotal();
                        WaitForNewEntry();
                        textBox1.Text = "0";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error..");
            }

        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                double valueEntered = 0;

                if (e.KeyData == Keys.Enter)
                {
                    if (comboBox1.Text == "ADD" && double.TryParse(textBox1.Text, out valueEntered))
                    {
                        label2.ForeColor = System.Drawing.Color.Black;
                        label2.Text = "New Total: " + tapeArray.Sum().ToString();
                        listBox1.Items.Add("+ " + textBox1.Text);
                        AddToArray(valueEntered);
                        WaitForNewEntry();
                        textBox1.Text = "0";
                    }
                    else if (comboBox1.Text == "SUBTRACT" && double.TryParse(textBox1.Text, out valueEntered))
                    {
                        label2.ForeColor = System.Drawing.Color.Black;
                        label2.Text = "History";
                        listBox1.Items.Add("- " + textBox1.Text);
                        AddToArray(Math.Abs(valueEntered) * -1);
                        WaitForNewEntry();
                        textBox1.Text = "0";
                    }
                    else if (comboBox1.Text == "MULTIPLY" && double.TryParse(textBox1.Text, out valueEntered))
                    {
                        label2.ForeColor = System.Drawing.Color.Black;
                        label2.Text = "History";
                        RolloverArray();
                        DisplayTotal();
                        listBox1.Items.Add("x " + valueEntered);
                        tapeArray[0] *= valueEntered;
                        DisplayTotal();
                        WaitForNewEntry();
                        textBox1.Text = "0";
                    }
                    else if (comboBox1.Text == "DIVIDE" && double.TryParse(textBox1.Text, out valueEntered))
                    {
                        label2.ForeColor = System.Drawing.Color.Black;
                        label2.Text = "History";
                        RolloverArray();
                        DisplayTotal();
                        listBox1.Items.Add("/ " + valueEntered);
                        tapeArray[0] /= valueEntered;
                        DisplayTotal();
                        WaitForNewEntry();
                        textBox1.Text = "0";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error..");
            }
        }


    }

                    
       
}
