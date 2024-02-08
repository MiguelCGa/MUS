'''
filtros HP y LP de un polo con frecuencia de corte variable 
'''
#%%

import sounddevice as sd
import soundfile as sf
import sys
import kbhit
import numpy as np


CHUNK = 1024

if len(sys.argv) < 2:
    #print("Plays a wave file.\n\nUsage: %s filename.wav" % sys.argv[0])
    #sys.exit(-1)
    wav = 'tormenta.wav'
else:
    wav = sys.argv[1]

#%%

data, SRATE = sf.read(wav,dtype="float32")

stream = sd.OutputStream(samplerate = SRATE, blocksize = CHUNK, channels = len(data.shape))  
stream.start()




kb = kbhit.KBHit()
frame = 0
c= ' '   


print("[F] frecuencia de corte +100")
print("[f] frecuencia de corte -100")
print("[l] filtro lp")
print("[h] filtro hp")

freq = 1000
prev = 0
filter = "lp"

while c!= 'q': # and not(quit):
    bloque = data[frame*CHUNK : (frame+1)*CHUNK]
        
    alpha = np.exp(-2*np.pi*freq / SRATE)    

    # filtro paso bajo
    if (filter=='lp'):
        bloque[0] = alpha * prev + (1-alpha) * bloque[0]
        for i in range(1,CHUNK):         
            bloque[i] = alpha * bloque[i-1] + (1-alpha) * bloque[i]            
    # filtro paso alto (diferencia entre señal original y paso bajo)
    elif (filter=='hp'):
        bloque[0] = bloque[0] - alpha * prev + (1-alpha) * bloque[0]
        for i in range(1,CHUNK):
            bloque[i] = bloque[i] - (alpha * bloque[i-1] + (1-alpha) * bloque[i])

    prev = bloque[CHUNK-1]

    stream.write(bloque)    
    if kb.kbhit():
        c = kb.getch()
        print(c)
        if c =='q': break
        elif c=='F': freq += 100
        elif c=='f': freq -= 100
        elif c=='l': filter = 'lp'
        elif c=='h': filter = 'hp'
        freq = min(SRATE/2,max(0,freq))
        print("Filtro: ", filter,"   Cut off frec: ",freq)

    frame += 1

kb.set_normal_term()

        
stream.stop()
stream.close()

#%%