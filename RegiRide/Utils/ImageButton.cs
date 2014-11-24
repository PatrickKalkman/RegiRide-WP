namespace RegiRide.Utils
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    /// <summary>
    /// ImageButton control
    /// </summary>
    public class ImageButton : Button
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageButton"/> class. 
        /// </summary>
        public ImageButton()
        {
            // Set template for the control
            this.DefaultStyleKey = typeof(ImageButton);
        }

        /// <summary>
        /// Normal State Image dependency property
        /// </summary>
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), null);

        /// <summary>
        /// Normal State Image property
        /// </summary>
        public ImageSource Image
        {
            get { return (ImageSource)this.GetValue(ImageProperty); }
            set { this.SetValue(ImageProperty, value); }
        }

        /// <summary>
        /// Pressed State Image dependency property
        /// </summary>
        public static readonly DependencyProperty PressedImageProperty = DependencyProperty.Register("PressedImage", typeof(ImageSource), typeof(ImageButton), null);

        /// <summary>
        /// Pressed State Image property
        /// </summary>
        public ImageSource PressedImage
        {
            get { return (ImageSource)this.GetValue(PressedImageProperty); }
            set { this.SetValue(PressedImageProperty, value); }
        }
    }
}
