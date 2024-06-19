using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using EletroCore;

ApplicationConfiguration.Initialize();

List<Charge> charges = [
    new Charge(10, 500, 500), 
    new Charge(-10, 550, 550)
];
DateTime dt = DateTime.Now;

void run()
{
    var frame = DateTime.Now;
    var time = frame - dt;
    Charge.Apply(charges, (float)time.TotalSeconds);
    dt = frame;
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
    run();
};

Application.Run(form);