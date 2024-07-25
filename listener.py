from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/', methods=['POST'])
def receive_data():
    content = request.get_json()
    print('Received content:', content)
    return jsonify({'message': 'Data received successfully'})

if __name__ == '__main__':
    # 监听所有接口
    app.run(host='127.0.0.1', port=5000)
