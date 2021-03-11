from flask import Flask
from flask import Response
from redis import StrictRedis
import json

app = Flask(__name__)
bdd = StrictRedis(host="localhost", port=6379, db=0, decode_responses=True)

@app.route("/like/<object_id>/<user_id>", methods = ['POST'])
def increase(object_id,user_id):

    oqr =  bdd.get("OBJECT::{}".format(object_id))
    likers = json.loads(oqr) if oqr is not None else []

    uqr = bdd.get("USER::{}".format(user_id))
    object_liked = json.loads(uqr) if uqr is not None else []

    if user_id not in likers:
        likers.append(user_id)
        bdd.set("OBJECT::{}".format(object_id), json.dumps(likers))

    if object_id not in object_liked:
        object_liked.append(object_id)
        bdd.set("USER::{}".format(user_id), json.dumps(object_liked))

    like_count = len(likers) if likers != None else 1
    body = {
        'object' : object_id,
        'user': user_id,
        'like_count' : like_count,
        'likers' :  likers,
        'object_liked': object_liked
    }

    return Response(json.dumps(body))



# COUNTER FOR 1 object
@app.route("/like/count/<object_id>", methods = ['GET'])
def getCount(object_id):

    oqr = bdd.get("OBJECT::{}".format(object_id))
    likers = json.loads(oqr) if oqr is not None else []

    like_count = len(likers) if likers != None else 0
    body = { 'like_count' : like_count }

    return Response(json.dumps(body))

# LIKERS FOR 1 object
@app.route("/like/list/<object_id>", methods = ['GET'])
def getLikers(object_id):

    oqr =  bdd.get("OBJECT::{}".format(object_id))
    likers = json.loads(oqr) if oqr is not None else []

    body = { 'likers' : likers }

    return Response(json.dumps(body))


# Objects FOR 1 liker
@app.route("/like/user/<user_id>", methods = ['GET'])
def getTravels(user_id):
    uqr =  bdd.get("USER::{}".format(user_id))
    object_liked = json.loads(uqr) if uqr is not None else []

    body = {
        "object_liked": object_liked
    }

    return Response(json.dumps(body))

if __name__ == "__main__":
    app.run(host="0.0.0.0", port=8080, debug=False)
