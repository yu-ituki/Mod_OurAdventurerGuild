# Readme
Our Adventurer Guild のModです。  
ヨトゥン戦の生贄が変身するまでのラウンド数を任意の数値に変更します。  
デフォルトは10ラウンドです。  
ラウンド数はBepInExのコンフィグファイルで編集可能です。  
コンフィグファイルは下記パスに存在します。  
[インストールフォルダ]/BepInEx/config/yu-ituki.oag.mod_save-jotunn-quest-sacrifice.cfg

ビルド手順や導入手順などは[一つ前のページ](https://github.com/yu-ituki/Mod_OurAdventurerGuild)に記載してあります。  

# ソース説明
SaveJotunnQuestSacrifice.cs がほぼ全てです。  
ヨトゥン戦の生贄はBattleMapTileInteractableのTimeLimitトリガーで作られているようなので、  
Awake時に捕虜っぽいやつを名前解決などで判定して残ラウンド数を変更しています。  


