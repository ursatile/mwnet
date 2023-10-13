
@echo off
echo Updating ZIP files...
del *.zip
for /d %%1 in (*) do 7z a mwnet%%1.zip ./%%1/rockaway/* -mx0 -xr!bin -xr!obj
echo All done! Yay!