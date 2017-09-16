using CL.Entity;
using CL.ExtensionMethods;
using CL.Views;
using System;
using System.Windows;

namespace CL.Controllers
{
    public abstract class ControllerBase<T>
        where T : IViewBase
    {
        public T View { get; set; }

        public abstract void LoadView();

        protected abstract void SetViewState();

        public Profile Gebruiker { get; set; }

        private bool _playingSound = false;

        protected void PlaySound(System.IO.UnmanagedMemoryStream soundStream)
        {
            if (_playingSound)
                return;

            _playingSound = true;

            System.Threading.ThreadPool.QueueUserWorkItem(x => {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(soundStream);
                sp.PlaySync();
                _playingSound = false;
            });
        }

        /// <summary>
        /// meant to take an anonymous method, that
        /// contains the statements to be wrapped in
        /// a try-catch containing default error handling.
        /// </summary>
        /// <param name="method"></param>
        public void Invoke(Action method)
        {
            try
            {
                method();
            }
            catch (Exception e)
            {
                View.ShowError(e);
                Application.Current.Shutdown();
            }
        }

    }
}
