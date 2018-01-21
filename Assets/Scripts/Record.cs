/*
 * Alot of this script was pulled off the following website: 
 * https://forum.unity.com/threads/detecting-musical-notes-from-vocal-input.316698/
 * This code that I got detects the pitch and displays the pitch. The following 
 * methods are methods that I added: everything but 2 line of code in the update,
 * everything in the CheckPitch() method except for the first if statement,
 * isCorrect(), nextNote(). Antyhing else that was not included in this list I 
 * pulled from the website above.
 * 
 * Jennifer Vang
 * 
 * */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
class Peak
{
    public float amplitude;
    public int index;

    public Peak()
    {
        amplitude = 0f;
        index = -1;
    }

    public Peak(float _frequency, int _index)
    {
        amplitude = _frequency;
        index = _index;
    }
}

class AmpComparer : IComparer<Peak>
{
    public int Compare(Peak a, Peak b)
    {
        return 0 - a.amplitude.CompareTo(b.amplitude);
    }
}

class IndexComparer : IComparer<Peak>
{
    public int Compare(Peak a, Peak b)
    {
        return a.index.CompareTo(b.index);
    }
}

public class Record : MonoBehaviour
{
    public float displayTimer;
    public int count;
    public bool correct;
    private float timer;

    [SerializeField]
    private Text correctDisplay;

    [SerializeField]
    private Text noteDisplay;

    [SerializeField]
    private Text lyricDisplay;

    [SerializeField]
    private ChangeLyrics changeLyricsScript;

    [SerializeField]
    private DisplayCorrect changeCorrectScript;

    [SerializeField]
    private BoatMovement bmScript;

    public float rmsValue;
    public float dbValue;
    public float pitchValue;

    public int qSamples = 1024;
    public int binSize = 1024; // you can change this up, I originally used 8192 for better resolution, but I stuck with 1024 because it was slow-performing on the phone
    public float refValue = 0.1f;
    public float threshold = 0.01f;

    private List<Peak> peaks = new List<Peak>();
    float[] samples;
    float[] spectrum;
    int samplerate;

    public Text display; // drag a Text object here to display values
    public bool mute = true;
    public AudioMixer masterMixer; // drag an Audio Mixer here in the inspector

    void Start()
    {
        count = 0;
        timer = 0;
        correct = false;

        samples = new float[qSamples];
        spectrum = new float[binSize];
        samplerate = AudioSettings.outputSampleRate;

        // starts the Microphone and attaches it to the AudioSource
        GetComponent<AudioSource>().clip = Microphone.Start(null, true, 10, samplerate);
        GetComponent<AudioSource>().loop = true; // Set the AudioClip to loop
        while (!(Microphone.GetPosition(null) > 0)) { } // Wait until the recording has started
        GetComponent<AudioSource>().Play();  
       
        // Mutes the mixer. You have to expose the Volume element of your mixer for this to work. I named mine "masterVolume".
        masterMixer.SetFloat("masterVolume", -80f);
    }

    void Update()
    {
        if (timer > 0){
            timer -= Time.deltaTime;

            if(count == 19){
                correctDisplay.text = "Good Singing! You were able to pull out of the current.";
                lyricDisplay.text = "WINNER!";
                bmScript.StopMovement();
            }
            else{
                correctDisplay.text = "Correct " + count;
            }  
        }
        else
        {
            if (bmScript.stop && count ==0){
                correctDisplay.text = "There is a waterfall ahead and the current is too strong, however, singing will move the boat.";
            }
            else{
                changeLyricsScript.ChangeTheLyrics(count);
                changeCorrectScript.ChangeText(count);
                AnalyzeSound();
                CheckPitch();
            }
        }
    }

    void AnalyzeSound()
    {
        float[] samples = new float[qSamples];
        GetComponent<AudioSource>().GetOutputData(samples, 0); // fill array with samples
        int i = 0;
        float sum = 0f;
        for (i = 0; i < qSamples; i++)
        {
            sum += samples[i] * samples[i]; // sum squared samples
        }
        rmsValue = Mathf.Sqrt(sum / qSamples); // rms = square root of average
        dbValue = 20 * Mathf.Log10(rmsValue / refValue); // calculate dB
        if (dbValue < -160) dbValue = -160; // clamp it to -160dB min

        // get sound spectrum
        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0f;
        for (i = 0; i < binSize; i++)
        { // find max
            if (spectrum[i] > maxV && spectrum[i] > threshold)
            {
                peaks.Add(new Peak(spectrum[i], i));
                if (peaks.Count > 5)
                { // get the 5 peaks in the sample with the highest amplitudes
                    peaks.Sort(new AmpComparer()); // sort peak amplitudes from highest to lowest
                    //peaks.Remove (peaks [5]); // remove peak with the lowest amplitude
                }
            }
        }
        float freqN = 0f;
        if (peaks.Count > 0)
        {
            //peaks.Sort (new IndexComparer ()); // sort indices in ascending order
            maxV = peaks[0].amplitude;
            int maxN = peaks[0].index;
            freqN = maxN; // pass the index to a float variable
            if (maxN > 0 && maxN < binSize - 1)
            { // interpolate index using neighbours
                var dL = spectrum[maxN - 1] / spectrum[maxN];
                var dR = spectrum[maxN + 1] / spectrum[maxN];
                freqN += 0.5f * (dR * dR - dL * dL);
            }
        }
        pitchValue = freqN * (samplerate / 2f) / binSize; // convert index to frequency
        peaks.Clear();
    }

    private void CheckPitch()
    {
        if (display != null) {
            display.text = ("Pitch: " + pitchValue.ToString("F0") + " Hz");
        }

        if (bmScript.zPos >= 88.8){
            timer = 0;
            correct = false;
            correctDisplay.text = "Game Over! Try again!";
        }
        else{
            if (pitchValue >= 252 && pitchValue <= 272 && count == 0 ||            // Row, 262
                    pitchValue >= 252 && pitchValue <= 272 && count == 1 ||         // row, 262
                    pitchValue >= 252 && pitchValue <= 272 && count == 2 ||         // row 262
                    pitchValue >= 284 && pitchValue <= 304 && count == 3 ||         // your 294
                    pitchValue >= 320 && pitchValue <= 340 && count == 4 ||         // boat 330
                    pitchValue >= 320 && pitchValue <= 340 && count == 5 ||         // gent- 330
                    pitchValue >= 284 && pitchValue <= 304 && count == 6 ||         // ly 294
                    pitchValue >= 320 && pitchValue <= 340 && count == 7 ||         // down 330
                    pitchValue >= 339 && pitchValue <= 359 && count == 8 ||         // the 349
                    pitchValue >= 382 && pitchValue <= 402 && count == 9 ||         // stream 392
                    pitchValue >= 513 && pitchValue <= 533 && count == 10 ||        // Merrily 523
                    pitchValue >= 382 && pitchValue <= 402 && count == 11 ||        // merrily 392
                    pitchValue >= 320 && pitchValue <= 340 && count == 12 ||        // merrily 330
                    pitchValue >= 252 && pitchValue <= 272 && count == 13 ||        // merrily 262
                    pitchValue >= 382 && pitchValue <= 402 && count == 14 ||        // life 392
                    pitchValue >= 339 && pitchValue <= 359 && count == 15 ||        // is 349
                    pitchValue >= 320 && pitchValue <= 340 && count == 16 ||        // but 330
                    pitchValue >= 284 && pitchValue <= 304 && count == 17 ||        // a 294
                    pitchValue >= 252 && pitchValue <= 272 && count == 18){       // dream 262         
                nextNote();
            }
            else{
                timer = 0;
                correct = false;
            }
        }
    }

    public bool isCorrect()
    {
        return correct;
    }

    private void nextNote()
    {
        count++;
        timer = displayTimer;
        correct = true;
    }
}
