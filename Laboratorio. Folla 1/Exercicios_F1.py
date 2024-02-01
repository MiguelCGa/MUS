#%% celda inicial para cargar librerías y definir constantes

import matplotlib.pyplot as plt
import numpy as np
import time

# gráficos en el notebook
%matplotlib inline 

SRATE = 44100 # Sample rate, para todo el programa


#%% Ex. 1

def noise(dur):   
    # emtpy necesita argumento int 
    a = np.empty(round(dur*SRATE))
    for i in range(a.size):
        # valor aleatorio en [-1, 1)
        a[i] = np.random.uniform(-1,1)
    return a

start = time.time()
dur = 10
a = noise(dur)
print(f'time: {time.time() - start}')

# round(dur*SRATE): numero de muestras de la señal
print(f'Num muestras: {round(dur*SRATE)}')
plt.plot(a[:20])



#%% Ex. 2. Una forma mucho más directa
# Delegamos todo el trabajo en NumPy
# Más eficiente

def noise2(dur):
    # uniform necesita int
    return np.random.uniform(-1,1,round(dur*SRATE))

start = time.time()
dur = 10
a = noise2(dur)
print(f'time: {time.time() - start}')


plt.plot(a[:20])


#%% Ex. 3

# arange no necesita conversion a int de SRATE*dur 
def osc(freq, dur=1, amp=1, phase=0):
    return np.sin(phase+np.arange(SRATE*dur)*2*np.pi*freq/SRATE) * amp

dur = 1
freq = 1
a = osc(freq, dur)

plt.plot(a)

#%% Ex. 4
# Miguel Curros Garcia
# Diego Lopez Balboa

# A Naranja, entre -1 y 1
# B Azul, entre 0 y 1
# Al variar A entre 0 y 1 tiene tambien un attack mucho menor y un sustain mayor que B debido a la forma de la onda
# además la onda A entre 0 y 1 esta una octava por debajo de la onda B entre -1 y 1, ya que A tiene la mitad de la frecuencia

def modulate(sample, freq):
    return sample * freq

# arange no necesita conversion a int de SRATE*dur
dur = 1
freq = 2
a = noise2(dur)

b = osc(freq)
plt.plot(modulate(a, 0.5+b*0.5))
plt.plot(modulate(a, b))

#%% Ex. 5

def notarmonicos(dur, freq, arm, amp):
    return sum([osc(i*freq, dur, amp/i) for i in range(1, arm+1)])

dur = 1
freq = 2
arm = 10
amp = 3
plt.plot(notarmonicos(dur, freq, arm, amp))

#%% Ex. 6
#%% Ex. 7
#%% Ex. 8