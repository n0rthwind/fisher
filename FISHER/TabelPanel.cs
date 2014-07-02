using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FISHER
{
    public partial class TablePanel : Panel
    {
        public TablePanel()
        {
            InitializeComponent();
        }

        public int CellWidth
        {
            get { return cellWidth; }
            set
            {
                cellWidth = value;
                Invalidate();
            }
        }
        int cellWidth;

        public int CellHeigth 
        {
            get { return cellHeigth; }
            set
            {
                cellHeigth = value;
                Invalidate();
            }
        }
        int cellHeigth;

        public int FirstColumn
        {
            get { return firstColumn; }
            set
            {
                firstColumn = value;
                Invalidate();
            }
        }
        int firstColumn;

        public int Columns
        {
            get { return columns; }
            set
            {
                columns = value;
                Invalidate();
            }
        }
        int columns;

        public int Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                Invalidate();
            }
        }
        int rows;

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                Invalidate();
            }
        }
        Color borderColor = Color.Black;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawRectangle(new Pen(new SolidBrush(borderColor)), 0, 0, Width-1, Height-1);

            for (int i = 1; i < rows; i++)
                e.Graphics.DrawLine(new Pen(new SolidBrush(borderColor)),
                    0, i * cellHeigth, Width, i * cellHeigth);

            for (int j = 1; j < columns; j++)
                e.Graphics.DrawLine(new Pen(new SolidBrush(borderColor)),
                    (firstColumn - cellWidth) + (j * cellWidth), 0,
                    (firstColumn - cellWidth) + (j * cellWidth), Height);
        }
    }
}
