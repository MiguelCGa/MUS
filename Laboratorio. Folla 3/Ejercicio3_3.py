#%%
from tkinter import *
import sounddevice as sd
from consts import *
from oscWaveTable import OscWaveTable
import numpy as np

root = Tk()
freq = 1.0
amp = 10.0
def motion(event):
    global freq, amp
    freq, amp = 100+1400.0*event.x/500.0, event.y/500.0

def play(stream, oscil, root):
    global freq, amp
    oscil.setFrec(freq)
    oscil.vol = amp
    chunk = oscil.getChunk()
    stream.write((np.float32)(chunk))
    root.after(10, play, stream, oscil, root)

oscil = OscWaveTable(freq, amp, CHUNK)
stream = sd.OutputStream(samplerate=SRATE, channels=1, blocksize=CHUNK)
stream.start()
root.after(10, play, stream, oscil, root)
root.bind('<Motion>', motion)
root.mainloop()
stream.stop()
stream.close()