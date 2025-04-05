using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VietLott
{
    public partial class choithu : Form
    {
        private List<Button> buttonsA = new List<Button>();
        private List<Button> buttonsB = new List<Button>();
        private List<Button> buttonsC = new List<Button>();
        private List<int> selectedA = new List<int>();
        private List<int> selectedB = new List<int>();
        private List<int> selectedC = new List<int>();
        private Random random = new Random();

        public choithu()
        {
            InitializeComponent();
            InitializeButtons();
        }

        private void InitializeButtons()
        {
            foreach (Control control in panelA.Controls)
                if (control is Button btn) { btn.Click += NumberButton_Click; buttonsA.Add(btn); }

            foreach (Control control in panelB.Controls)
                if (control is Button btn) { btn.Click += NumberButton_Click; buttonsB.Add(btn); }

            foreach (Control control in panelC.Controls)
                if (control is Button btn) { btn.Click += NumberButton_Click; buttonsC.Add(btn); }
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Parent is Panel panel)
            {
                int number = int.Parse(btn.Text);
                List<int> selectedList = GetSelectedList(panel);
                Label label = GetLabel(panel);

                if (selectedList.Contains(number))
                {
                    selectedList.Remove(number);
                    btn.BackColor = SystemColors.Control;
                }
                else if (selectedList.Count < 6)
                {
                    selectedList.Add(number);
                    btn.BackColor = Color.Red;
                }
                label.Text = string.Join(", ", selectedList);
            }
        }

        

        private List<Button> GetButtonList(Panel panel)
        {
            if (panel == panelA) return buttonsA;
            if (panel == panelB) return buttonsB;
            return buttonsC;
        }

        private List<int> GetSelectedList(Panel panel)
        {
            if (panel == panelA) return selectedA;
            if (panel == panelB) return selectedB;
            return selectedC;
        }

        private Label GetLabel(Panel panel)
        {
            if (panel == panelA) return lblA;
            if (panel == panelB) return lblB;
            return lblC;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void chkHUYA_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button119_Click(object sender, EventArgs e)
        {

        }

        private void button137_Click(object sender, EventArgs e)
        {

        }


        private void btn_TuChonSoA_Click(object sender, EventArgs e)
        {
            ChonSoNgauNhien(selectedA, buttonsA, lblA);
        }

        private void btn_TuChonSoB_Click(object sender, EventArgs e)
        {
            ChonSoNgauNhien(selectedB, buttonsB, lblB);
        }

        
        private void ChonSoNgauNhien(List<int> selectedList, List<Button> buttons, Label label)
        {
            selectedList.Clear();
            foreach (Button btn in buttons)
            {
                btn.BackColor = SystemColors.Control; // Reset màu cho tất cả nút
            }

            foreach (Button btn in buttons.OrderBy(_ => random.Next()).Take(6))
            {
                selectedList.Add(int.Parse(btn.Text));
                btn.BackColor = Color.Red;
            }

            label.Text = string.Join(", ", selectedList);
        }

        private void btn_TuChonSoC_Click(object sender, EventArgs e)
        {
            ChonSoNgauNhien(selectedC, buttonsC, lblC);
        }

        private void lblB_Click(object sender, EventArgs e)
        {

        }

        private void btn_QuaySo_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu cả ba bộ số đều chưa nhập
            if (selectedA.Count == 0 && selectedB.Count == 0 && selectedC.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn số cho bất kỳ bộ nào!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Dừng hàm nếu chưa có bộ số nào được chọn
            }

            // Tạo danh sách 6 số ngẫu nhiên từ 1 đến 45
            List<int> randomNumbers = Enumerable.Range(1, 45).OrderBy(_ => random.Next()).Take(6).ToList();

            // Hiển thị kết quả trong lblResult
            lblResult.Text = "Kết quả: " + string.Join(", ", randomNumbers);

            // Kiểm tra các bộ số A, B, C có trúng số nào không
            List<int> matchedA = selectedA.Intersect(randomNumbers).ToList();
            List<int> matchedB = selectedB.Intersect(randomNumbers).ToList();
            List<int> matchedC = selectedC.Intersect(randomNumbers).ToList();

            // Tạo thông báo trúng thưởng
            string resultMessage = "Không trúng giải!";
            if (matchedA.Any() || matchedB.Any() || matchedC.Any())
            {
                resultMessage = "Trúng các số:\n";
                if (matchedA.Any()) resultMessage += $"A: {string.Join(", ", matchedA)}\n";
                if (matchedB.Any()) resultMessage += $"B: {string.Join(", ", matchedB)}\n";
                if (matchedC.Any()) resultMessage += $"C: {string.Join(", ", matchedC)}\n";
            }

            // Hiển thị thông báo trúng số
            MessageBox.Show(resultMessage, "Kết Quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}
