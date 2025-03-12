# Readme
Our Adventurer Guild のModです。  
入手金額をn倍にします。  
デフォルトは1.5倍です。  
係数値はBepInExのコンフィグファイルで編集可能です。  
コンフィグファイルは下記パスに存在します。  
[インストールフォルダ]/BepInEx/config/yu-ituki.oag.mod_money-factor.cfg

なお付与タイミングで係数を掛けている関係で、一部のUI表記に反映されない場合があります、ご了承ください。  
（クエストメニューなどでUI側では変わらず「1000ゴールド取得」と書かれてますが、係数1.5なら内部的にはちゃんと1500もらえます）  

ビルド手順や導入手順などは[一つ前のページ](https://github.com/yu-ituki/Mod_OurAdventurerGuild)に記載してあります。  


# ソース説明
MoneyFactor.cs でHubManager::AddGold() を PrefixPatch してます。  
単に取得金額に掛け算してるだけです。  

