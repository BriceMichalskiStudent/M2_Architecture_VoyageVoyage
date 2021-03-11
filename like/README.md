# Like Service

---

## [POST] /like/<object_key>/<user_key>

> No body needed

**exemple**

request:

```bash
curl -X POST localhost:8080/like/voyage_toulouse/brice
```

response:

```JSON
{
  "object": "voyage_toulouse",
  "user": "brice",
  "like_count": 1,
  "likers": [
    "brice"
  ],
  "object_liked": [
    "voyage_toulouse"
  ]
}
```

---

## [GET] /like/count/<object_key>

**exemple**

request:

```bash
curl -X GET localhost:8080/like/count/voyage_toulouse
```

response:

```JSON
{
  "like_count": 1
}
```

---

## [GET] /like/list/<object_key>

**exemple**

request:

```bash
curl -X GET localhost:8080/like/list/voyage_toulouse
```

response:

```JSON
{
  "likers": [
    "brice"
  ]
}
```

---

## [GET] /like/user/<user_key>

**exemple**

request:

```bash
curl -X GET localhost:8080/like/user/brice
```

response:

```JSON
{
  "object_liked": [
    "voyage_toulouse"
  ]
}
```
