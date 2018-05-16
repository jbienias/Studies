import random
import math
import sys

#Jan Bienias 238201

def encode(sPlaintext, iNumBits):
	byte_array = bytearray(sPlaintext, 'utf-16')
	z = []
	k = iNumBits//8
	j = -1 * k
	num = 0
	for i in range( len(byte_array) ):
		if i % k == 0:
			j += k
			num = 0
			z.append(0)
		z[j//k] += byte_array[i]*(2**(8*(i%k)))
	return z

def decode(aiPlaintext, iNumBits):
	bytes_array = []
	k = iNumBits//8
	for num in aiPlaintext:
		for i in range(k):
			temp = num
			for j in range(i+1, k):
				temp = temp % (2**(8*j))
			letter = temp // (2**(8*i))
			bytes_array.append(letter)
			num = num - (letter*(2**(8*i)))
	decodedText = bytearray(b for b in bytes_array).decode("utf-8", "ignore")
	return decodedText

def generate_keys(iNumBits=256, iConfidence=32):
	p = find_prime(iNumBits, iConfidence)
	g = find_primitive_root( p )
	x = random.randint( 1, p )
	h = modexp( g, x, p )
	publicKey = PublicKey(p, g, h, iNumBits)
	privateKey = PrivateKey(p, g, x, iNumBits)
	return {'privateKey': privateKey, 'publicKey': publicKey}

def encrypt(p, g, x, h, iNumBits, sPlaintext):
	z = encode(sPlaintext, iNumBits)
	cipher_pairs = []
	for i in z:
		y = random.randint( 0, p )
		c = modexp( g, y, p )
		d = (i*modexp( h, y, p)) % p
		cipher_pairs.append( [c, d] )

	encryptedStr = ""
	for pair in cipher_pairs:
		encryptedStr += str(pair[0]) + ' ' + str(pair[1]) + ' '

	return encryptedStr

def decrypt(keyp, keyx, cipher):
	plaintext = []
	cipherArray = cipher.split()
	if (not len(cipherArray) % 2 == 0):
			return "Malformed Cipher Text"
	for i in range(0, len(cipherArray), 2):
			c = int(cipherArray[i])
			d = int(cipherArray[i+1])
			s = modexp( c, keyx, keyp )
			plain = (d*modexp( s, keyp-2, keyp)) % keyp
			plaintext.append( plain )
	decryptedText = decode(plaintext, 256)
	decryptedText = "".join([ch for ch in decryptedText if ch != '\x00'])
	return decryptedText

def verify_sign(p, a, y, r, s, m):
	if r < 1 or r > p-1 : return False
	v1 = pow(y,r,p)%p * pow(r,s,p)%p
	v2 = pow(a,m,p)
	return v1 == v2

def gcd( a, b ):
	if b != 0:
		return gcd( b, a % b )
	return a

def modexp( base, exp, modulus ):
	return pow(base, exp, modulus)

def mul_inv(a, b):
	b0 = b
	x0, x1 = 0, 1
	if b == 1: return 1
	while a > 1:
	    q = a / b
	    a, b = b, a%b
	    x0, x1 = x1 - q * x0, x0
	if x1 < 0: x1 += b0
	return x1

def generate_sign(p, g, x, m):
	while 1:
		k = random.randint(1,p-2)
		if gcd(k,p-1)==1: break
	r = pow(g,k,p)
	l = mul_inv(k, p-1)
	s = l*(m-x*r)%(p-1)
	return r,s

def main():

	if sys.argv[1] == "-g":
		p = 1665997633093155705263923663680487185948531888850484859473375695734301776192932338784530163
		g = 170057347237941209366519667629336535698946063913573988287540019819022183488419112350737049
		elg = open("elgamal.txt", "w")
		elg.write('%s\n%s' % (str(p), str(g)))

	if sys.argv[1] == "-k":
		index = 0
		elga = open("elgamal.txt", "r")
		for line in elga:
			if index == 0: p = int(line)
			elif index == 1: g = int(line)
			index = index + 1
		x = random.randint( 1, p )
		h = modexp( g, x, p )
		epriv = open("private.txt", "w")
		epriv.write('%s\n%s\n%s' % (str(p), str(g), str(x)))
		epub = open("public.txt", "w")
		epub.write('%s\n%s\n%s' % (str(p), str(g), str(h)))

	if sys.argv[1] == "-e":
		index = 0
		epub = open("public.txt", "r")
		for line in epub:
			if index == 2: public_h = int(line)
			index = index + 1
		index = 0
		epriv = open("private.txt", "r")
		for line in epriv:
			if index == 0: private_p = int(line)
			if index == 1: private_g = int(line)
			if index == 2: private_x = int(line)
			index = index + 1
		iNumBits = 256
		eplain = open("plain.txt", "r")
		for line in eplain:
			message = str(line)
		cipher = encrypt(private_p, private_g, private_x, public_h, iNumBits, message)
		ecrypto = open("crypto.txt", "w")
		ecrypto.write('%s' % (str(cipher)))

	if sys.argv[1] == "-d":
		index = 0
		epriv = open("private.txt", "r")
		for line in epriv:
			if index == 0: private_p = int(line)
			if index == 2: private_x = int(line)
			index = index + 1
		ecrypt = open("crypto.txt", "r")
		for line in ecrypt:
			cipher = str(line)
		decrypted = u''.join((decrypt(private_p, private_x, cipher))).encode('utf-8').strip()
		edecrypt = open("decrypt.txt", "w")
		edecrypt.write('%s' % (decrypted))

	if sys.argv[1] == "-v":
		index = 0
		signature = open("signature.txt", "r")
		for line in signature:
			if index == 0: signature_r = int(line)
			if index == 1: signature_s = int(line)
			index = index + 1
		message = open("message.txt", "r")
		for line in message:
			content = long(line, base = 36)
		index = 0
		epub = open("public.txt", "r")
		for line in epub:
			if index == 0: public_p = int(line)
			if index == 1: public_g = int(line)
			if index == 2: public_y = int(line)
			index = index + 1
		isvalid = verify_sign(public_p, public_g, public_y, signature_r, signature_s, content)
		verify = open("verify.txt", "w")
		verify.write('Verification: %s' % isvalid)
		print('Veryfication: %s' % isvalid)

	if sys.argv[1] == "-s":
		index = 0
		epriv = open("private.txt", "r")
		for line in epriv:
			if index == 0: private_p = int(line)
			if index == 1: private_g = int(line)
			if index == 2: private_x = int(line)
			index = index + 1
		message = open("message.txt", "r")
		for line in message:
			content = long(line, base = 36) #latwo wychodzi za granice, mala wiadomosc przejdzie!
		rr, ss = generate_sign(private_p, private_g, private_x, content)
		signature = open("signature.txt", "w")
		signature.write('%s\n%s' % (str(rr), str(ss)))

main()
