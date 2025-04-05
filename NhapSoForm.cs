using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VietLott
{
    public partial class NhapSoForm : Form
    {
        public List<int> SelectedNumbers { get; private set; } = new List<int>(); // Danh sách số đã chọn
        private List<Button> numberButtons; // Danh sách button số
        public NhapSoForm(List<int> selectedNumbers)
        {
            InitializeComponent();

            SelectedNumbers = new List<int>(selectedNumbers);

        

            // Khởi tạo danh sách button số (bạn cần đặt đúng tên button)
            numberButtons = new List<Button>
            {
                tron1, tron2, tron3, tron4, tron5, tron6, tron7, tron8, tron9, tron10,
                tron11, tron12, tron13, tron14, tron15, tron16, tron17, tron18, tron19, tron20,
                tron21, tron22, tron23, tron24, tron25, tron26, tron27, tron28, tron29, tron30,
                tron31, tron32, tron33, tron34, tron35, tron36, tron37, tron38, tron39, tron40,
                tron41, tron42, tron43, tron44, tron45
            };

            // Gán sự kiện Click cho tất cả các button số
            foreach (var btn in numberButtons)
            {
                btn.Click += NumberButton_Click;
            }

            // Hiển thị lại số đã chọn trước đó
            HighlightSelectedNumbers();

            btn_XacNhan.Enabled = SelectedNumbers.Count == 6;
        }
        private void HighlightSelectedNumbers()
        {
            foreach (var btn in numberButtons)
            {
                int number = int.Parse(btn.Text);
                if (SelectedNumbers.Contains(number))
                {
                    btn.BackColor = Color.Red;  // Tô màu đỏ cho số đã chọn
                }
                else
                {
                    btn.BackColor = SystemColors.Control;  // Trả về màu mặc định
                }
            }
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            if (sender is Button clickedButton)
            {
                int number = int.Parse(clickedButton.Text);

                if (SelectedNumbers.Contains(number))
                {
                    SelectedNumbers.Remove(number);
                    clickedButton.BackColor = SystemColors.Control;
                }
                else if (SelectedNumbers.Count < 6)
                {
                    SelectedNumbers.Add(number);
                    clickedButton.BackColor = Color.Red;
                }

                btn_XacNhan.Enabled = SelectedNumbers.Count == 6;
            }
        }
        private void btn_XoaNhapLai_Click(object sender, EventArgs e)
        {
            // Xóa hết số đã chọn và reset màu của button
            foreach (var btn in numberButtons)
            {
                btn.BackColor = SystemColors.Control;
            }

            SelectedNumbers.Clear();
            btn_XacNhan.Enabled = false;
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            if (SelectedNumbers.Count == 6)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void tron2_Click(object sender, EventArgs e)
        {

        }

        private void tron3_Click(object sender, EventArgs e)
        {

        }

        private void tron4_Click(object sender, EventArgs e)
        {

        }

        private void tron5_Click(object sender, EventArgs e)
        {

        }

        private void tron6_Click(object sender, EventArgs e)
        {

        }

        private void tron7_Click(object sender, EventArgs e)
        {

        }

        private void tron8_Click(object sender, EventArgs e)
        {

        }

        private void tron9_Click(object sender, EventArgs e)
        {

        }



        private void tron10_Click(object sender, EventArgs e)
        {

        }

        private void tron11_Click(object sender, EventArgs e)
        {

        }

        private void tron12_Click(object sender, EventArgs e)
        {

        }

        private void tron13_Click(object sender, EventArgs e)
        {

        }

        private void tron14_Click(object sender, EventArgs e)
        {

        }

        private void tron15_Click(object sender, EventArgs e)
        {

        }

        private void tron31_Click(object sender, EventArgs e)
        {

        }

        private void tron2_Click_1(object sender, EventArgs e)
        {

        }

        private void tron1_Click_1(object sender, EventArgs e)
        {

        }

        private void tron30_Click(object sender, EventArgs e)
        {

        }

        private void tron40_Click(object sender, EventArgs e)
        {

        }

        private void tron32_Click(object sender, EventArgs e)
        {

        }

        private void tron33_Click(object sender, EventArgs e)
        {

        }

        private void tron41_Click(object sender, EventArgs e)
        {

        }

        private void tron34_Click(object sender, EventArgs e)
        {

        }

        private void tron35_Click(object sender, EventArgs e)
        {

        }

        private void tron42_Click(object sender, EventArgs e)
        {

        }

        private void tron36_Click(object sender, EventArgs e)
        {

        }

        private void tron37_Click(object sender, EventArgs e)
        {

        }

        private void tron43_Click(object sender, EventArgs e)
        {

        }

        private void tron38_Click(object sender, EventArgs e)
        {

        }

        private void tron39_Click(object sender, EventArgs e)
        {

        }

        private void tron44_Click(object sender, EventArgs e)
        {

        }

        private void tron16_Click(object sender, EventArgs e)
        {

        }

        private void tron17_Click(object sender, EventArgs e)
        {

        }

        private void tron18_Click(object sender, EventArgs e)
        {

        }

        private void tron19_Click(object sender, EventArgs e)
        {

        }

        private void tron20_Click(object sender, EventArgs e)
        {

        }

        private void tron21_Click(object sender, EventArgs e)
        {

        }

        private void tron22_Click(object sender, EventArgs e)
        {

        }

        private void tron23_Click(object sender, EventArgs e)
        {

        }

        private void tron24_Click(object sender, EventArgs e)
        {

        }

        private void tron25_Click(object sender, EventArgs e)
        {

        }

        private void tron26_Click(object sender, EventArgs e)
        {

        }

        private void tron27_Click(object sender, EventArgs e)
        {

        }

        private void tron28_Click(object sender, EventArgs e)
        {

        }

        private void tron29_Click(object sender, EventArgs e)
        {

        }

        private void tron45_Click(object sender, EventArgs e)
        {
            
        }
    }
}
