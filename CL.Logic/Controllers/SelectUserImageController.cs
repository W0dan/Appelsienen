using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL.Views;

namespace CL.Controllers
{
    public class SelectUserImageController : ControllerBase<ISelectUserImageView>
    {
        private int _imageNr = 0;

        public override void LoadView()
        {
            _imageNr = Gebruiker.Image;

            View.LoadImages();
        }

        protected override void SetViewState()
        {

        }

        public int Image
        {
            get
            {
                return _imageNr;
            }
            set
            {
                _imageNr = value;
            }
        }

    }
}
