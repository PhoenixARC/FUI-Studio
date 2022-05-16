using FUI_Studio.Classes.fui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FourJ.UserInterface;

namespace FUI_Studio.Classes
{
    public class FuiShapeWrapper
    {
        public static Image CreateImageFromShape(Shape shape, FUIFile fui)
        {
            var components = fui.shapeComponents;
            var verts = fui.verts;
            var fuiBitmaps = fui.bitmaps;
            var size = shape.GetSize();
            var img = new Bitmap(size.Width, size.Height);
            using (var graphics = Graphics.FromImage(img))
            {
                for (int i = 0; i < shape.ComponentCount; i++)
                {
                    var component = components[i + shape.ComponentIndex];
                    switch (component.fillInfo.Type)
                    {
                        case FillStyle.eFuiFillType.COLOR:
                            PointF[] points = new PointF[component.vertCount];
                            for (int j = 0; j < component.vertCount; j++)
                            {
                                points[j] = verts[component.vertIndex + j].GetPointF();
                            }
                            Brush brush = new SolidBrush(component.fillInfo.Color.GetColor());
                            graphics.FillPolygon(brush, points);
                            break;

                        case FillStyle.eFuiFillType.BITMAP:
                            var startPoint = verts[component.vertIndex].GetPointF();
                            var endPoint = verts[component.vertIndex + 2].GetPointF();
                            RectangleF rect = new RectangleF(startPoint, new SizeF(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y));
                            FuiBitmap fuiBitmap = fuiBitmaps[component.fillInfo.BitmapIndex];
                            var componentImg = fui.Images[fui.bitmaps.IndexOf(fuiBitmap)];
                            var drawImage = new Bitmap(new MemoryStream(componentImg));
                            if ((int)fuiBitmap.format < 6)
                                ImageProcessor.ReverseColorRB(drawImage);
                            graphics.DrawImage(drawImage, rect);
                            break;
                        default:
                            break;
                    }
                }
            }
            return img;
        }
    }
}
