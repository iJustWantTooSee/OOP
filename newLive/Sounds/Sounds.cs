using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;

namespace newLive.Sounds
{
    public static class Effects
    {
        private static SoundPlayer _clickThanos = new SoundPlayer(@"..\..\Sounds\thanos.wav");
        private static SoundPlayer _speechLuntik = new SoundPlayer(@"..\..\Sounds\luntik.wav");
        private static SoundPlayer _soundDie = new SoundPlayer(@"..\..\Sounds\die.wav");
        private static SoundPlayer _soundStart = new SoundPlayer(@"..\..\Sounds\start.wav");
        private static SoundPlayer _soundOfTheEnd = new SoundPlayer(@"..\..\Sounds\theend.wav");
        private static SoundPlayer _soundPlant = new SoundPlayer(@"..\..\Sounds\plant.wav");
        private static SoundPlayer _soundDeadPlant = new SoundPlayer(@"..\..\Sounds\deadplant.wav");
        private static SoundPlayer _soundVylet = new SoundPlayer(@"..\..\Sounds\vylet.wav");


        public  static void PlaySpeechTanos()
        {
            Task.Run(() =>
            {
                _clickThanos.Play();
               // _clickThanos.Dispose();
            });

        }

        public async static void PlaySpeechLuntik()
        {
            await Task.Run(() =>
            {
                _speechLuntik.Play();
                _speechLuntik.Dispose();
            });
        }

        public async static void PlaySoundDie()
        {
            await Task.Run(() =>
            {
                _soundDie.Play();
                _soundDie.Dispose();
            });
        }

        public async static void PlaySoundStart()
        {
            await Task.Run(() =>
            {
                _soundStart.Play();
                _soundStart.Dispose();
            });
        }

        public async static void PlaySoundOfTheEnd()
        {
            await Task.Run(() =>
            {
                _soundOfTheEnd.Play();
                _soundOfTheEnd.Dispose();
            });
        }

        public static void PlaySoundPlant()
        {
            Task.Run(() =>
            {
                _soundPlant.Play();
                _soundPlant.Dispose();
            });
        }

        public async static void PlaySoundDeadPlant()
        {
            await Task.Run(() =>
            {
                _soundDeadPlant.Play();
                _soundDeadPlant.Dispose();
            });
        }

        public async static void PlaySoundVylet()
        {
            await Task.Run(() =>
            {
                _soundVylet.Play();
                _soundVylet.Dispose();
            });
        }

    }
}
