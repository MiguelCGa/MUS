'''
simple reproductor con filtro de agudos
'''


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



data, SRATE = sf.read(wav,dtype="float32")

stream = sd.OutputStream(samplerate = SRATE, blocksize = CHUNK, channels = len(data.shape))  
stream.start()


#%% 
# Experimento: numpy primero calcula todo el lado dcho y luego asigna al izdo
import numpy as np
b = np.arange(10,dtype="float32")
alpha =0.5

# esta versión no funciona como queremos
print('Experimento: ')
b[1:] = b[0:-1] + alpha*(b[1:]-b[0:-1]) 
b[0]  = 0 + alpha*(b[0]-0)

# esta si
c = np.arange(10,dtype="float32")
c[0] = 0 + alpha * (c[0]-0)
for i in range(1,len(c)):
    c[i] = c[i-1] + alpha * (c[i]-c[i-1])

# comparamos resultados
print(b)
print(c)

#%%

kb = kbhit.KBHit()

frame = 0
c= ' '

# factor filtro: alpha 1 anula filtro, 0 (delay-> cambio de fase), 0.5 media  muestras
alpha = 0.5
print(f"[A/a] sube baja alpha: {alpha}")



mem = 0
while c!= 'q':     
    # ojo: bloque referencia a data -> modificamos data!
    bloque = data[frame*CHUNK:(frame+1)*CHUNK]
    # si no queremos tocar data habría que hacer copia con
    # bloque = np.copy(data[frame*CHUNK:(frame+1)*CHUNK])

    bloque = data[frame*CHUNK:(frame+1)*CHUNK]
    if len(bloque)==0: break

    bloque[0] = mem + alpha * (bloque[0]-mem)
    for i in range(1,CHUNK):
        bloque[i] = bloque[i-1] + alpha * (bloque[i]-bloque[i-1])
    
    mem = bloque[CHUNK-1]
    stream.write(bloque)


    # ESto no funciona: numpy primero calcula todo el lado dcho y luego 
    # asigna al lado izdo
    #ultSample=bloque[-1] # guardamos ultima muestra para siguiente frame
    #bloque[1:] = bloque[0:-1] + alpha*(bloque[1:]-bloque[0:-1]) 
    #bloque[0]  = mem + alpha*(bloque[0]-mem)                                           
    #mem = ultSample # actualizamos memo con ultima muestra
    #stream.write(bloque)    
    
    if kb.kbhit():
        c = kb.getch()
        print(c)
        if c =='q': break
        elif c=='A': alpha += 0.1
        elif c=='a': alpha -= 0.1
        alpha = min(2.0,max(0.1,alpha))
        print(f"[A/a] sube baja alpha: {alpha}")


    frame += 1


stream.stop()       
stream.close()
kb.set_normal_term()
