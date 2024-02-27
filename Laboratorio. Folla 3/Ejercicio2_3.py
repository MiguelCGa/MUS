#%%

import numpy as np
import osc
import matplotlib.pyplot as plt
import sounddevice as sd
from consts import *

class OscFMEj2:
    def __init__(self,fc=110.0,amp=1.0,fm=6.0, beta=1.0, bm = 1.0):
        self.fc = fc
        self.amp = amp
        self.fm = fm
        self.beta = beta
        self.frame = 0
        self.bmrange = 0.05
        self.fbm = 1.0

        # moduladora = βsin(2πfm)
        self.mod = osc.Osc(freq=fm,amp=beta)
        self.modmod = osc.Osc(freq=fm, amp=bm)
        
    def next(self):  
        # sin(2πfc+mod)  
        # sacamos el siguiente chunk de la moduladora
        self.modmod.freq = self.fbm.get()
        mod = self.mod.next()
        modmod = self.modmod.next()
        modmod *= self.bmrange.get()
        mod += modmod

        # soporte para el chunk de salida
        sample = np.arange(self.frame,self.frame+CHUNK)
        # aplicamos formula
        out =  self.amp*np.sin(2*np.pi*self.fc*sample/SRATE + mod)
        self.frame += CHUNK
        return (np.float32)(out) 


# prueba de slider
import slider
from tkinter import *
oscil = OscFMEj2()

def playSound(stream, oscil, tk):
    stream.write(oscil.next())
    tk.after(10, playSound, stream, oscil, tk)

stream = sd.OutputStream(samplerate=SRATE, channels=1, blocksize=CHUNK)
stream.start()

tk = Tk()
oscil.fbm = slider.Slider(tk, "Beta Mod Freq", 0.0, 1000.0)
oscil.bmrange = slider.Slider(tk, "Beta Mod Range", 1.0, 0.0)
tk.after(10, playSound, stream, oscil, tk)
tk.mainloop()

stream.stop()
stream.close()
    