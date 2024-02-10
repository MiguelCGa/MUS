# Miguel Curros García
# Diego López Balboa

#%%
from Osc import Osc
import sounddevice as sd
import soundfile as sf
import kbhit

SRATE = 48000
CHUNK = 1024

def testOsc(osc):
    stream = sd.OutputStream(samplerate=SRATE, blocksize=CHUNK, channels=1)
    stream.start()
    kb = kbhit.KBHit()
    key = kb.getKey()
    while (key != "escape"):
        samples = osc.next()
        stream.write(samples)
        key = kb.getKey()
        if (key!=' '):
            if (key == "F"):
                osc.setFreq(osc.getFreq() * 9 / 8)
            elif (key == "f"):
                osc.setFreq(osc.getFreq() * 8 / 9)
            elif (key == "V"):
                osc.setVol(osc.getVol() * 2)
            elif (key == "v"):
                osc.setVol(osc.getVol() / 2)
    
    kb.quit()
    stream.stop()
    stream.close()



#%%
osc = Osc(261.63, 1, 0)

testOsc(osc)