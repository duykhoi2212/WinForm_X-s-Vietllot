using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel; // Thêm thư viện này để hiển thị thuộc tính trong Properties

public class Tron : Button
{
    [Category("Custom Properties")]
    [Description("Màu viền của Button")]
    public Color BorderColor { get; set; } = Color.Black;  // Màu viền (mặc định: đen)

    [Category("Custom Properties")]
    [Description("Độ dày viền của Button")]
    public int BorderSize { get; set; } = 2;              // Độ dày viền (mặc định: 2px)

    protected override void OnPaint(PaintEventArgs pevent)
    {
        Graphics graphics = pevent.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias; // Làm mịn đường viền

        // Vẽ nền tròn
        using (SolidBrush brush = new SolidBrush(this.BackColor))
        {
            graphics.FillEllipse(brush, 0, 0, this.Width, this.Height);
        }

        // Vẽ viền tròn
        using (Pen pen = new Pen(BorderColor, BorderSize))
        {
            graphics.DrawEllipse(pen, BorderSize / 2, BorderSize / 2, this.Width - BorderSize, this.Height - BorderSize);
        }

        // Đặt vùng button là hình tròn
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(0, 0, this.Width, this.Height);
        this.Region = new Region(path);

        // Vẽ chữ ở giữa button
        TextRenderer.DrawText(graphics, this.Text, this.Font, new Rectangle(0, 0, this.Width, this.Height), this.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
    }
}
