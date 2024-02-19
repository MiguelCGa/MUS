#%%

import kbhit_pygame as kbhit
import sounddevice as sd
import soundfile as sf
import scipy as sc
import numpy as np

SRATE = 48000
CHUNK = 1024

KEYS = {
    "z": 1.0,
    "x": 9.0/8.0,
    "c": 5.0/4.0,
    "v": 4.0/3.0,
    "b": 3.0/2.0,
    "n": 5.0/3.0,
    "m": 15.0/8.0,
    "q": 2.0,
    "w": 17.0/8.0,
    "e": 9.0/4.0,
    "r": 7.0/4.0,
    "t": 5.0/2.0,
    "y": 8.0/3.0,
    "u": 23.0/8.0,
}

class Piano:
    def __init__(self):
        self.pianoSound = 'piano.wav'
        self.data, self.fs = sf.read(self.pianoSound, dtype='float32')
        self.stream = sd.OutputStream(samplerate=SRATE, blocksize=CHUNK, channels=1)

    def __del__(self):
        self.stream.stop()
        self.stream.close()

    def playKey(self, key):
        newnote = sc.signal.resample(self.data, (int)(len(self.data)/KEYS[key]))
        #sd.playasync(newnote, self.fs)
        self.stream.write(newnote)








def playPiano():
    piano = Piano()
    kb = kbhit.KBHit()
    key = ''
    while (key != "escape"):
        key = kb.getKey()
        if (not key):
            key= ' '
        if key in KEYS:
            piano.playKey(key)
    kb.quit()

playPiano()