# Diego López Balboa
# Miguel Curros García
#%%
import kbhit_pygame as kbhit
import sounddevice as sd
import soundfile as sf
from scipy import signal
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
    "e": 10.0/4.0,
    "r": 8.0/3.0,
    "t": 6.0/2.0,
    "y": 10.0/3.0,
    "u": 30.0/8.0,
}


class NotaPiano:
    def __init__(self, data, relFreq):
        self.note = signal.resample(data, (int)(len(data)/relFreq))
        self.stream = sd.OutputStream(samplerate=SRATE, blocksize=CHUNK, channels=1)
        self.stream.start()
        self.last = len(self.note)

    def __del__(self):
        self.stream.stop()
        self.stream.close()

    def next(self):
        if (self.last < len(self.note)):
            self.sample = self.note[self.last:self.last+CHUNK]
            self.last += CHUNK
            self.stream.write(self.sample)

    def play(self):
        self.last = 0

class Piano:
    def __init__(self):
        self.pianoSound = 'piano.wav'
        self.data, self.fs = sf.read(self.pianoSound, dtype='float32')
        self.notes = {}
        for key in KEYS:
            self.notes[key] = NotaPiano(self.data, KEYS[key])

    def next(self):
        for key in KEYS:
            self.notes[key].next()

    def play(self, key):
        self.notes[key].play()



def playPiano():
    piano = Piano()
    kb = kbhit.KBHit()
    key = ''
    while (key != "escape"):
        key = kb.getKey()
        if key in KEYS:
            piano.play(key)
        piano.next()

    kb.quit()

playPiano()
# %%
