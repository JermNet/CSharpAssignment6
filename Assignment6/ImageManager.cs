﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Assignment6
{
    public class ImageManager
    {
        public void GetImage(Canvas firstCanvas, Canvas secondCanvas, int xLoc1, int yLoc1, BitmapImage BMimg)
        {
            if (firstCanvas != null)
            {
                RemoveImagesWithSubstring(firstCanvas, BMimg);
                AddImageToCanvas(firstCanvas, xLoc1, yLoc1, BMimg);
            }

            if (secondCanvas != null)
            {
                AddImageToCanvas(secondCanvas, xLoc1, yLoc1, BMimg);
            }
        }

        private void AddImageToCanvas(Canvas canvas, int x, int y, BitmapImage BMimg)
        {
            Image img = new Image { Source = BMimg, Width = BMimg.Width, Height = BMimg.Height };
            Canvas.SetLeft(img, x);
            Canvas.SetTop(img, y);
            canvas.Children.Add(img);
        }

        private void RemoveImagesWithSubstring(Canvas canvas, BitmapImage BMimg)
        {
            string[] substrings = { "P1", "P2", "P3", "P4" };
            foreach (var substring in substrings)
            {
                if (BMimg.ToString().Contains(substring))
                {
                    RemoveImagesWithSubstring(canvas, substring);
                    break;
                }
            }
        }

        private void RemoveImagesWithSubstring(Canvas canvas, string substring)
        {
            List<UIElement> toRemove = new List<UIElement>();

            foreach (UIElement child in canvas.Children)
            {
                if (child is Image img && img.Source != null && img.Source.ToString().Contains(substring))
                {
                    toRemove.Add(img);
                }
            }

            foreach (UIElement img in toRemove)
            {
                canvas.Children.Remove(img);
            }
        }
    }
}