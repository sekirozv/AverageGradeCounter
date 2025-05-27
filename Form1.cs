using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvgScore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
        }

        private void Calculate()
        {
            try
            {
                int a = int.Parse(textBox1.Text); 
                int b = int.Parse(textBox2.Text);
                int c = int.Parse(textBox3.Text);
                int d = int.Parse(textBox4.Text); 
                int totalGrades = a + b + c + d;
                if (totalGrades == 0)
                {
                    score.Text = "";
                    rating.Text = "";
                    quantity.Text = "";
                    return;
                }

                double avg = (5 * a + 4 * b + 3 * c + 2 * d) / (double)totalGrades;
                score.Text = Math.Round(avg, 2).ToString();

                string recommendedGrade;
                if (avg >= 4.5)
                {
                    recommendedGrade = "5";
                    rating.ForeColor = Color.DarkGreen;
                }
                else if (avg >= 3.5)
                {
                    recommendedGrade = "4";
                    rating.ForeColor = Color.Green;
                }
                else if (avg >= 2.5)
                {
                    recommendedGrade = "3";
                    rating.ForeColor = Color.Orange;
                }
                else
                {
                    recommendedGrade = "2";
                    rating.ForeColor = Color.Red;
                }

                rating.Text = recommendedGrade;
                quantity.Text = totalGrades.ToString();
            }
            catch
            {
                return;
            }
        }

        private void p5_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox1.Text) + 1;
            textBox1.Text = value.ToString();
            Calculate();
        }

        private void m5_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox1.Text) - 1;
            if (value < 0) value = 0;
            textBox1.Text = value.ToString();
            Calculate();
        }

        private void c5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            Calculate();
        }

        private void p4_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox2.Text) + 1;
            textBox2.Text = value.ToString();
            Calculate();
        }

        private void m4_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox2.Text) - 1;
            if (value < 0) value = 0;
            textBox2.Text = value.ToString();
            Calculate();
        }

        private void c4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "0";
            Calculate();
        }

        private void p3_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox3.Text) + 1;
            textBox3.Text = value.ToString();
            Calculate();
        }

        private void m3_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox3.Text) - 1;
            if (value < 0) value = 0;
            textBox3.Text = value.ToString();
            Calculate();
        }

        private void c3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "0";
            Calculate();
        }

        private void p2_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox4.Text) + 1;
            textBox4.Text = value.ToString();
            Calculate();
        }

        private void m2_Click(object sender, EventArgs e)
        {
            int value = int.Parse(textBox4.Text) - 1;
            if (value < 0) value = 0;
            textBox4.Text = value.ToString();
            Calculate();
        }

        private void c2_Click(object sender, EventArgs e)
        {
            textBox4.Text = "0";
            Calculate();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
            textBox4.Text = "0";
            score.Text = "";
            rating.Text = "";
            quantity.Text = "";
        }

        private void copyBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(score.Text))
            {
                string result = $"Средний балл: {score.Text}\nРекомендуемая оценка: {rating.Text}\nКоличество оценок: {quantity.Text}";
                Clipboard.SetText(result);
                MessageBox.Show("Результаты скопированы в буфер обмена!", "Копирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private string saveFilePath = "grade_results.txt"; 

        private string GetResultString()
        {
            if (string.IsNullOrEmpty(score.Text)) return string.Empty;

            return $"Средний балл: {score.Text}\n" +
                   $"Рекомендуемая оценка: {rating.Text}\n" +
                   $"Количество оценок: {quantity.Text}\n" +
                   $"Пятерок: {textBox1.Text}\n" +
                   $"Четверок: {textBox2.Text}\n" +
                   $"Троек: {textBox3.Text}\n" +
                   $"Двоек: {textBox4.Text}\n" +
                   $"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}";
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string result = GetResultString();
            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Нет данных для сохранения!", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                File.AppendAllText(saveFilePath, result + "\n\n");
                MessageBox.Show($"Результаты успешно сохранены в файл! Файл: {saveFilePath}", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
