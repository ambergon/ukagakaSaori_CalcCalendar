# ukagakaSaori_CalcCalendar
伺か用 日付計算を簡単にするためのSAORI-Universal


## 使い方
例はyayaです。
FUNCTIONEXを使用し呼び出します。
```
_res = FUNCTIONEX( "PATH" , "計算したい年" , "計算したい月" , "計算したい日" , "追加したい年" , "追加したい月" , "追加したい日" )
_res = FUNCTIONEX( "./dll/ukagakaSaori_CalcCalendar.dll" , "2023" , "4" , "1" , "1" , "2" , "3" )

返り値は4種類あります。
単純な返り値 Saori Result 相当
-> _res = 2024/6/4

それぞれValue0 , Value1 , Value2 相当
-> valueex0 = 2024
-> valueex1 = 6
-> valueex2 = 4


//マイナスも可能です。
_res = FUNCTIONEX( "./dll/ukagakaSaori_CalcCalendar.dll" , "2023" , "4" , "1" , "1" , "2" , "-3" )
-> _res = 2024/5/29

```
里々でも同じように使用できると思いますが引数の受け渡し方法を知らないためいい感じにしてください。


## 動作確認環境
Windows 10
SSP/2.6.51


## 注意事項
このプログラムを使ったいかなる問題や損害に対して、私は責任を負いません。


## License
MIT

## Author
ambergon
