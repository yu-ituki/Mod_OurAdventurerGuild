# がいよう
Vortexの拡張機能を誰も作ってなかったので作った。  
とりあえず↓のバージョンを持ってきた。  
[BepInEx v5.4.23.2](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2)    

そもそもVortexってなんだよという人は↓。  
https://www.nexusmods.com/about/vortex  

# インストールほうほう
1. Vortexをインストールして、一回起動してまた閉じて
2. AppData\Roaming\Vortex\plugins 以下にgame-ouradventurerguildフォルダをそのまま放り込んで
3. Vortexを起動すると
4. VortexのGames上に「Our Adventurer Guild」という名前で出てくるので
5. それをManagedでいつも通りVortexっぽく使える

ちなみに2は同梱の「deploy_local.bat」を叩くことでも出来る。  

# せつめい
なんかようわからんが↓に書いてあった方法で追加した。  
大体コピペだけど、ゲームIDとかをちょろっと書き換えたり、prepareForModding()でBepInExのコピーを走らせたりだけ変えてる。   
https://github.com/Nexus-Mods/Vortex/wiki/MODDINGWIKI-Developers-General-Creating-a-game-extension  

# インストールほうほうの補足
vortexの拡張機能はシンボリックリンクは対応してないっぽいので注意。  
ジャンクションも駄目だった。  
そのためdeploy_local.batではxcopyで直ファイルコピーしている。  

