using System.Drawing;
using System.Windows.Forms;

ApplicationConfiguration.Initialize();


void run()
{
    
}


var form = new Form
{
    WindowState = FormWindowState.Maximized,
    FormBorderStyle = FormBorderStyle.None
};
form.KeyDown += (o, e) =>
{
    switch (e.KeyCode)
    {
        case Keys.Escape:
            form.Close();
            break;
    }
};

var tm = new Timer();
tm.Tick += (o, e) => form.Invalidate();;
tm.Interval = 25;
form.Load += (o, e) => tm.Start();

form.Paint += (o, e) =>
{
    var g = e.Graphics;

    var voidColor = Color.FromArgb(15, 15, 40);
    g.Clear(voidColor);
};

Application.Run(form);