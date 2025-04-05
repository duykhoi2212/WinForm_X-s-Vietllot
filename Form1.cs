using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace VietLott
{
    public partial class Form1 : Form
    {
        private List<int> selectedNumbers = new List<int>(); // Danh sách số đã chọn
        private List<Button> soButtons = new List<Button>();
        private List<List<Button>> allSoButtons = new List<List<Button>>(); // Danh sách bộ số

        public Form1()
        {
            InitializeComponent();
            LoadComboBoxData();
            groupBox_ChonBoSoo.Visible = false; // Ẩn groupBox chọn bộ số ban đầu
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Cấu hình ListView
            listView_KetQuaa.View = View.Details;
            listView_KetQuaa.Columns.Clear();
            listView_KetQuaa.Columns.Add("Ngày Quay", 100);
            listView_KetQuaa.Columns.Add("Kỳ Quay", 80);
            listView_KetQuaa.Columns.Add("Kết Quả", 200);
        }

        // 📌 Load dữ liệu vào ComboBox (tránh trùng lặp)
        private void LoadComboBoxData()
        {
            string filePath = Path.Combine(Application.StartupPath, "KetQuaXoSo.txt");
            if (File.Exists(filePath))
            {
                HashSet<string> uniqueItems = new HashSet<string>();
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(new[] { ',' }, 3);
                    if (parts.Length >= 3)
                    {
                        string ngay = parts[0].Trim();
                        string ky = parts[1].Replace("Kỳ ", "").Trim();
                        string ngayKy = $"{ngay} - {ky}";
                        if (!uniqueItems.Contains(ngayKy))
                        {
                            uniqueItems.Add(ngayKy);
                            comboBox_NgayKy.Items.Add(ngayKy);
                        }
                    }
                }
                if (comboBox_NgayKy.Items.Count > 0)
                    comboBox_NgayKy.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Không tìm thấy file KetQuaXoSo.txt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox_NgayKy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox_NgayKy.SelectedItem == null) return;
            string selectedText = comboBox_NgayKy.SelectedItem.ToString().Trim();
            listView_KetQuaa.Items.Clear();
            string filePath = Path.Combine(Application.StartupPath, "KetQuaXoSo.txt");
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                bool found = false;
                foreach (string line in lines)
                {
                    string[] parts = line.Split(new[] { ',' }, 3);
                    if (parts.Length >= 3)
                    {
                        string ngay = parts[0].Trim();
                        string ky = parts[1].Replace("Kỳ ", "").Trim();
                        string ketQua = parts[2].Replace("Kết quả: ", "").Trim();
                        string ngayKy = $"{ngay} - {ky}";
                        if (ngayKy == selectedText)
                        {
                            ListViewItem item = new ListViewItem(ngay);
                            item.SubItems.Add(ky);
                            item.SubItems.Add(ketQua);
                            listView_KetQuaa.Items.Add(item);
                            found = true;
                            break;
                        }
                    }
                }
                if (!found)
                {
                    MessageBox.Show($"Không tìm thấy kết quả cho '{selectedText}'!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("File KetQuaXoSo.txt không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 📌 Quay số và lưu kết quả
        private void btn_QuaySo_Click(object sender, EventArgs e)
        {
            List<int> randomNumbers = GenerateRandomNumbers();
            DateTime ngayQuay = DateTime.Now;
            int kyQuay = new Random().Next(1000, 9999);
            ListViewItem item = new ListViewItem(ngayQuay.ToString("dd/MM/yyyy"));
            item.SubItems.Add(kyQuay.ToString());
            item.SubItems.Add(string.Join(" - ", randomNumbers));
            listView_KetQuaa.Items.Add(item);
            SaveResultToFile(ngayQuay, kyQuay, randomNumbers);
            string comboBoxItem = $"{ngayQuay:dd/MM/yyyy} - {kyQuay}";
            if (!comboBox_NgayKy.Items.Contains(comboBoxItem))
            {
                comboBox_NgayKy.Items.Add(comboBoxItem);
            }
        }

        // 📌 Ghi kết quả vào file
        private void SaveResultToFile(DateTime ngayQuay, int kyQuay, List<int> numbers)
        {
            string filePath = Path.Combine(Application.StartupPath, "KetQuaXoSo.txt");
            try
            {
                if (!File.Exists(filePath))
                {
                    File.Create(filePath).Close();
                }
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    string resultLine = $"{ngayQuay:dd/MM/yyyy}, Kỳ {kyQuay}, Kết quả: {string.Join(" - ", numbers)}";
                    writer.WriteLine(resultLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 📌 Tạo 6 số ngẫu nhiên không trùng
        private List<int> GenerateRandomNumbers()
        {
            Random random = new Random();
            HashSet<int> numbers = new HashSet<int>();
            while (numbers.Count < 6)
            {
                int num = random.Next(1, 46);
                numbers.Add(num);
            }
            return numbers.ToList();
        }

        // 📌 Xóa số đã chọn
        private void btn_XoaSoChon_Click(object sender, EventArgs e)
        {
            tron1.Text = tron2.Text = tron3.Text = tron4.Text = tron5.Text = tron6.Text = "";
        }

        private void btn_MoiNhapSo_Click(object sender, EventArgs e)
        {
            using (NhapSoForm nhapSoForm = new NhapSoForm(selectedNumbers))
            {
                if (nhapSoForm.ShowDialog() == DialogResult.OK)
                {
                    selectedNumbers = nhapSoForm.SelectedNumbers;
                    if (selectedNumbers.Count == 6)
                    {
                        tron1.Text = selectedNumbers[0].ToString();
                        tron2.Text = selectedNumbers[1].ToString();
                        tron3.Text = selectedNumbers[2].ToString();
                        tron4.Text = selectedNumbers[3].ToString();
                        tron5.Text = selectedNumbers[4].ToString();
                        tron6.Text = selectedNumbers[5].ToString();
                    }
                }
            }
        }

        // 📌 Nhập số tự động
        private void btn_NhapSoTudong_Click(object sender, EventArgs e)
        {
            selectedNumbers = GenerateRandomNumbers();
            tron1.Text = selectedNumbers[0].ToString();
            tron2.Text = selectedNumbers[1].ToString();
            tron3.Text = selectedNumbers[2].ToString();
            tron4.Text = selectedNumbers[3].ToString();
            tron5.Text = selectedNumbers[4].ToString();
            tron6.Text = selectedNumbers[5].ToString();
        }

        // 📌 Thêm bộ số
        private void btn_ThemBoSo_Click(object sender, EventArgs e)
        {
            if (allSoButtons.Count >= 2)
            {
                MessageBox.Show("Bạn chỉ có thể thêm tối đa 2 bộ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            List<Button> soButtons = new List<Button>();
            int yOffset = 420 + (allSoButtons.Count * 70);
            for (int i = 0; i < 6; i++)
            {
                Button btn = new Button
                {
                    Size = new Size(50, 50),
                    Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold),
                    ForeColor = Color.Red,
                    Text = "?",
                    Location = new Point(50 + i * 55, yOffset)
                };
                btn.Click += BtnNhapSo_Click;
                this.Controls.Add(btn);
                soButtons.Add(btn);
            }
            allSoButtons.Add(soButtons);
        }

        private void BtnNhapSo_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;
            List<int> currentNumbers = allSoButtons
                .SelectMany(set => set)
                .Where(btn => int.TryParse(btn.Text, out _))
                .Select(btn => int.Parse(btn.Text))
                .ToList();
            using (NhapSoForm formNhap = new NhapSoForm(currentNumbers))
            {
                if (formNhap.ShowDialog() == DialogResult.OK)
                {
                    List<int> selectedNumbers = formNhap.SelectedNumbers;
                    if (selectedNumbers.Count == 6)
                    {
                        int index = allSoButtons.SelectMany(set => set).ToList().IndexOf(clickedButton);
                        int groupIndex = index / 6;
                        if (groupIndex < allSoButtons.Count)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                allSoButtons[groupIndex][i].Text = selectedNumbers[i].ToString();
                            }
                        }
                    }
                }
            }
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            List<int> newNumbers = GenerateRandomNumbers();
            foreach (var btn in allSoButtons.SelectMany(set => set))
            {
                if (btn == clickedButton)
                {
                    btn.Text = newNumbers[0].ToString();
                    break;
                }
            }
        }

        // 📌 Sự kiện nhấn "Dò số" -> Hiển thị groupBox chọn bộ số
        private void btn_DoSo_Click(object sender, EventArgs e)
        {
            bool coBoSo = false;

            // 📌 Kiểm tra Bộ số 1
            if (!string.IsNullOrWhiteSpace(tron1.Text) &&
                !string.IsNullOrWhiteSpace(tron2.Text) &&
                !string.IsNullOrWhiteSpace(tron3.Text) &&
                !string.IsNullOrWhiteSpace(tron4.Text) &&
                !string.IsNullOrWhiteSpace(tron5.Text) &&
                !string.IsNullOrWhiteSpace(tron6.Text))
            {
                coBoSo = true;
            }

            // 📌 Kiểm tra các bộ số khác trong allSoButtons
            foreach (var boSo in allSoButtons)
            {
                if (boSo.All(btn => !string.IsNullOrWhiteSpace(btn.Text) && btn.Text != "?"))
                {
                    coBoSo = true;
                    break;
                }
            }

            // 📌 Nếu không có bộ số hợp lệ, thông báo lỗi
            if (!coBoSo)
            {
                MessageBox.Show("Bạn chưa nhập đủ 6 số vào bất kỳ bộ số nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 📌 Hiển thị GroupBox chọn bộ số
            groupBox_ChonBoSoo.Visible = true;

            // Nếu chỉ có bộ số 1, tự động chọn Bộ số 1
            if (allSoButtons.Count == 0)
            {
                radioButton_BoSo1.Checked = true;
            }
        }

        // 📌 Hàm dò số khi chọn bộ số
        private void DoSo()
        {
            if (comboBox_NgayKy.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn ngày và kỳ quay!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xác định bộ số nào được chọn
            int selectedBoSoIndex = -1;
            if (radioButton_BoSo1.Checked) selectedBoSoIndex = 0;
            else if (radioButton_BoSo2.Checked) selectedBoSoIndex = 1;
            else if (radioButton_BoSo3.Checked) selectedBoSoIndex = 2;

            if (selectedBoSoIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn một bộ số để dò!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<int> boSo = new List<int>();

            if (selectedBoSoIndex == 0) // ✅ Bộ số 1 lấy từ tron1 - tron6
            {
                Button[] tronButtons = { tron1, tron2, tron3, tron4, tron5, tron6 };

                foreach (var tron in tronButtons)
                {
                    if (int.TryParse(tron.Text, out int so))
                    {
                        boSo.Add(so);
                    }
                }
            }
            else if (selectedBoSoIndex >= 1 && selectedBoSoIndex <= allSoButtons.Count) // ✅ Bộ số 2, 3 lấy từ danh sách button
            {
                List<Button> selectedButtonSet = allSoButtons[selectedBoSoIndex - 1]; // Chỉnh sửa chỉ mục
                boSo.Clear();
                foreach (Button btn in selectedButtonSet)
                {
                    if (int.TryParse(btn.Text, out int so))
                    {
                        boSo.Add(so);
                    }
                }
            }
            else
            {
                MessageBox.Show("Bộ số bạn chọn chưa được nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (boSo.Count != 6)
            {
                MessageBox.Show("Bộ số chưa đủ 6 số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 📌 Lấy ngày và kỳ quay từ ComboBox
            string selectedNgayKy = comboBox_NgayKy.SelectedItem.ToString();
            string[] ngayKyParts = selectedNgayKy.Split(new[] { '-' }, 2);
            string ngayChon = ngayKyParts[0].Trim();
            string kyChon = ngayKyParts[1].Trim();

            // 📌 Đọc file kết quả xổ số
            string filePath = Path.Combine(Application.StartupPath, "KetQuaXoSo.txt");
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Không tìm thấy file kết quả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string ketQuaXoSo = "";
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(new[] { ',' }, 3);
                if (parts.Length >= 3)
                {
                    string ngay = parts[0].Trim();
                    string ky = parts[1].Replace("Kỳ ", "").Trim();
                    string ketQua = parts[2].Replace("Kết quả: ", "").Trim();
                    if (ngay == ngayChon && ky == kyChon)
                    {
                        ketQuaXoSo = ketQua;
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(ketQuaXoSo))
            {
                MessageBox.Show("Không tìm thấy kết quả cho ngày và kỳ quay đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 📌 Chuyển kết quả xổ số thành danh sách số
            List<int> soTrungThuong = ketQuaXoSo.Split(new[] { " - " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            // 📌 So sánh bộ số với kết quả
            int soTrung = boSo.Intersect(soTrungThuong).Count();

            // 📌 Hiển thị kết quả
            string message = soTrung == 6 ? "🎉 Chúc mừng! Bạn đã trúng jackpot! 🎉" :
                            soTrung > 0 ? $"Bạn đã trúng {soTrung} số!" :
                            "Rất tiếc, bạn không trúng số nào.";
            MessageBox.Show(message, "Kết quả dò số", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 📌 Gọi hàm DoSo() khi chọn bộ số
        private void radioButton_BoSo1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_BoSo1.Checked)
            {
                DoSo(); // Gọi hàm dò số khi chọn bộ số 1
            }
        }

        private void radioButton_BoSo2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_BoSo2.Checked)
            {
                DoSo(); // Gọi hàm dò số khi chọn bộ số 2
            }
        }

        private void radioButton_BoSo3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_BoSo3.Checked)
            {
                DoSo(); // Gọi hàm dò số khi chọn bộ số 3
            }
        }


        private void btn_Close_Click(object sender, EventArgs e)
        {
            groupBox_ChonBoSoo.Visible = false;
        }

        
    }
}
