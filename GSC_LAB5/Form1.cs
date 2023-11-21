using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;

namespace GSC_LAB5
{

    public partial class Form1 : Form
    {
        float dx = 0, dy = 0;
        float sx = 0.004f, sy = 0.0033f;
        float directX = -1;
        float directY = -1;
        private GLControl glControl1;
        public Form1()
        {
            InitializeComponent();
            glControl1 = new GLControl();
            //создаются обработчики событий для glControl
            glControl1.Resize += GLControl_Resize; // события Resize
            glControl1.Load += GLControl_Load;
            glControl1.Paint += GLControl_Paint;
            glControl1.Dock = DockStyle.Fill;
            pictureBox1.Controls.Add(glControl1);
        }

        private void GLControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl1.Width, glControl1.Height);
        }
        private void GLControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(57f/255f, 40f / 255f, 237f / 255f, 1);
            
        }
        private void GLControl_Paint(object sender, PaintEventArgs e)
        {
            // очистка буферов цвета и глубины
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            // настройка ортогональной проекции

            float ratio = (float)(glControl1.Width / (float)glControl1.Height);
            if (ratio > 1)
                GL.Ortho(0.0, 30.0 * ratio, 0.0, 30.0, -10, 10);
            else
                GL.Ortho(0.0, 30.0 / ratio, 0.0, 30.0, -10, 10);
            //песок
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(242f / 255f, 208f / 255f, 39f / 255f);
            GL.Vertex2(0,0);
            GL.Vertex2(0, 9);
            GL.Vertex2(30, 9);
            GL.Vertex2(30, 0);
            GL.End();
            //болото
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(10f/255f, 154f / 255f, 173f / 255f);
            GL.Vertex2(0, 9);
            GL.Vertex2(0, 16.5);
            GL.Vertex2(30, 16.5);
            GL.Vertex2(30, 9);
            GL.End();
            //типо небо
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(84f / 255f, 235f / 255f, 255f / 255f);
            GL.Vertex2(0, 16.5);
            GL.Vertex2(0, 30);
            GL.Vertex2(30, 30);
            GL.Vertex2(30, 16.5);
            GL.End();
            //Солнце
            GL.Color3(252f / 255f, 229f / 255f, 23f / 255f);
            DrawCircle(4.5, 27, 2);
            //Облака
            GL.Color3(228f / 255f, 241f / 255f, 242f / 255f);
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(9, 22.5);
            GL.Vertex2(9, 25.5);
            GL.Vertex2(15, 25.5);
            GL.Vertex2(15, 22.5);
            GL.End();
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(15, 25.5);
            GL.Vertex2(15, 28.5);
            GL.Vertex2(18, 28.5);
            GL.Vertex2(18, 25.5);
            GL.End();
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(19.5, 24);
            GL.Vertex2(19.5, 25.5);
            GL.Vertex2(22.5, 25.5);
            GL.Vertex2(22.5, 24);
            GL.End();
            GL.Begin(PrimitiveType.Polygon);
            GL.Vertex2(21, 27);
            GL.Vertex2(21, 28.5);
            GL.Vertex2(27, 28.5);
            GL.Vertex2(27, 27);
            GL.End();
            //Волны
            GL.LineWidth(4f);
            GL.Color3(230f / 255f, 247f / 255f, 242f / 255f);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex2(3,13.5); GL.Vertex2(7.5,13.5);
            GL.Vertex2(9,12); GL.Vertex2(13.5,12);
            GL.Vertex2(12,15); GL.Vertex2(15,15);
            GL.Vertex2(18,10.5); GL.Vertex2(21,10.5);
            GL.Vertex2(18,13.5); GL.Vertex2(27,13.5);
            GL.End();

            //Камни
            GL.Begin(PrimitiveType.TriangleFan); // веер треугольников
            GL.Color3(0.8f, 0.8f, 0.8f);//242f/255f, 208f/255f, 39f/255f - песок
            DrawCircle(3.75,5.25,1);
            GL.Color3(161f/255f, 154f / 255f, 119f / 255f);
            DrawCircle(11.25,6.75, 1);
            GL.Color3(161f / 255f, 154f / 255f, 119f / 255f);
            DrawCircle(12.75, 2.25, 1);
            GL.Color3(161f / 255f, 154f / 255f, 119f / 255f);
            DrawCircle(17.25, 6.75, 1);
            GL.Color3(161f / 255f, 154f / 255f, 119f / 255f);
            DrawCircle(18.75, 5.25, 1);
            GL.Color3(161f / 255f, 154f / 255f, 119f / 255f);
            DrawCircle(27.75, 6.75, 1);
            GL.PopMatrix();
            glControl1.SwapBuffers();
            glControl1.Invalidate();
        }

        private void DrawCircle(double xc, double yc, double r)
        {
            double x, y;
            GL.Begin(PrimitiveType.TriangleFan); // веер треугольников
       
            GL.Vertex2(xc + 0.8f, yc + 0.3);
            GL.Vertex2(xc + 1, yc);
            for (int i = 0; i <= 30; i++)
            {
                //GL.Color3(0f, 0f, 1.0f);
                x = xc + r * Math.Sin(i * Math.PI / 15);
                y = yc + r * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x, y);
            }
            GL.End();
        }
        /*GL.Color3(1f, 0, 0); // текущий цвет - красный (от 0 до 1)
                                 // режим рисования закрашенных треугольников
            GL.Begin(PrimitiveType.Triangles);
            GL.Vertex2(15, 15); GL.Vertex2(13, 13); GL.Vertex2(15, 1);
            GL.Vertex2(13, 13); GL.Vertex2(15, 15); GL.Vertex2(1, 15);
            GL.Vertex2(1, 15); GL.Vertex2(15, 15); GL.Vertex2(13, 17);
            GL.Vertex2(13, 17); GL.Vertex2(15, 15); GL.Vertex2(15, 29);
            GL.Vertex2(15, 29); GL.Vertex2(15, 15); GL.Vertex2(17, 17);
            GL.Vertex2(17, 17); GL.Vertex2(15, 15); GL.Vertex2(29, 15);
            GL.Vertex2(29, 15); GL.Vertex2(17, 13); GL.Vertex2(15, 15);
            GL.Vertex2(15, 15); GL.Vertex2(15, 1); GL.Vertex2(17, 13);
            GL.End();
            GL.LineWidth(4f); // толщина линий
            GL.Color3(0.7f, 0.2f, 0.8f);
            GL.Begin(PrimitiveType.LineLoop); // замкнутая линия
            GL.Vertex2(15, 1);
            GL.Vertex2(12, 12);
            GL.Vertex2(1, 15);
            GL.Vertex2(12, 18);
            GL.Vertex2(15, 29);
            GL.Vertex2(18, 18);
            GL.Vertex2(29, 15);
            GL.Vertex2(18, 12);
            GL.End();
            // граница
            GL.Color3(0f, 0f, 1.0f);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex2(1, 1); GL.Vertex2(1, 29);
            GL.Vertex2(29, 29); GL.Vertex2(29, 1);
            GL.End();
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity(); // Единичная матрица
            GL.Translate(dx, dy, 0);
            // шарик
            double xc = 15, yc = 15, r = 1;
            double x, y;
            GL.Begin(PrimitiveType.TriangleFan); // веер треугольников
            GL.Color3(0.4f, 0.5f, 1.0f);
            GL.Vertex2(xc + 0.8f, yc + 0.3);
            GL.Vertex2(xc + 1, yc);
            for (int i = 0; i <= 30; i++)
            {
                GL.Color3(0f, 0f, 1.0f);
                x = xc + r * Math.Sin(i * Math.PI / 15);
                y = yc + r * Math.Cos(i * Math.PI / 15);
                GL.Vertex2(x, y);
            }
            GL.End();
            if (dx <= -13) directX = 1;
            if (dx > 13) directX = -1;
            dx += directX * sx;
            if (dy <= -13) directY = 1;
            if (dy > 13) directY = -1;
            dy += directY * sy;*/
    }
}