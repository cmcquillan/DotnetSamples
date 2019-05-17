@echo off

rem Make a new public-private key pair
sn -k keyfile.snk

rem Extract public key to file
sn -p keyfile.snk public.snk

rem Copy the keyfile to library directory
copy keyfile.snk StrongNameLibrary
