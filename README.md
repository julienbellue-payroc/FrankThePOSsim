# FrankThePOSsim

![alt text](https://github.com/jbellue/FrankThePOSsim/blob/main/Frank.png?raw=true)

![deployment badge](https://github.com/jbellue/FrankThePOSsim/actions/workflows/deploy-frank.yaml/badge.svg)

Frank is a POS simulator. He likes to eat ticks and fleas.

When needed, he can send 💤REST💤 and 🧼SOAP🧼 requests, hopefully all the ones needed to test eConduit (and then some!)

When Frank sends the `Options` field (used in `getUserSelection`) he splits it at the commas. So `User option 1,User option 2,User option 3` will be sent as `Options=User option 1&Options=User option 2&Options=User option 1` in a SOAP request.

If you need to send a comma in the `Options` field, don't.