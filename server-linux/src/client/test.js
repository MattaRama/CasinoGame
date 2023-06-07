const Net = require('net');

const REQ = {
    type: 'user.transaction',
    pin: '4257',
    quantity: 1000000,
    physicalCurrency: true
};

console.log('Creating Connection...');
const soc = Net.createConnection({'host': 'localhost', 'port': 6978});
console.log('Connection Established. Sending...');

soc.on('data', (buf) => {
    console.log(buf.toString());
});

soc.on('ready', () => {
    setTimeout(() => {
        soc.write(JSON.stringify(REQ));
    }, 2000);
    
    process.stdin.on('data', (buf) => {
        var raw = buf.toString().trim();
        soc.write(raw);
    });
});