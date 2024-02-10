# Miguel Curros García
# Diego López Balboa

#%%

from OscStereo import OscStereo
import sounddevice as sd
import soundfile as sf
import kbhit_pygame as kbhit

SRATE = 48000
CHUNK = 1024

def testOscStereo(osc):
    stream = sd.OutputStream(samplerate=SRATE, blocksize=CHUNK, channels=2)
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
            # Controles para cambiar el volumen de cada canal
            elif (key == "L" or key == "l"):
                osc.setStereoDial(osc.getStereoDial() - 0.2)
            elif (key == "R" or key == "r"):
                osc.setStereoDial(osc.getStereoDial() + 0.2)
            elif (key == "C" or key == "c"):
                osc.setStereoDial(0)

    kb.quit()
    stream.stop()
    stream.close()


#%%
osc = OscStereo(261.63, 1, 0)

testOscStereo(osc)