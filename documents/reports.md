<h1 align="center">Thuật toán</h1>

### Ghi chú: Các công việc cần làm

- Cách tính toán các tiệm cận trong một bài toán cụ thể
- Cách viết mã giả(không quan trọng lắm)
- Tìm hiểu bài toán đệ quy
- Các tính độ phức tạp trng bài toán đệ quy

---

### 1. Loop invariants

- `Loop invariants`(bất biến vòng lặp) là điều kiện về mối quan hệ giữa các biến trong chương trình mà nó chắc chắn đúng ngay trước và ngay sau mỗi lần lặp(iteration) của vòng lặp(loop). Một `Loop invariants` có một số tính chất sau
  - `Initialization`: Nó đúng trước vòng lặp đầu tiên
  - `Maintenance`: Nếu nó đúng ở vòng lặp phía trước, nó sẽ tiếp tục đúng ở vòng lặp tiếp theo
  - `Termination`: Khi vòng lặp kết thúc, sự bât biến này có thể cho chúng ta các tính chất để chứng minh sự đúng đắn của thuật toán

---

### 2. Phân tích tiệm cận

- Tiệm cận trong phân tích thuật toán được dùng để ước lượng thời gian chạy của một giải thuật. Khi một thuật toán được cài đặt, bước phân tích tiệm cận sẽ giúp chúng ta đánh giá được các trường hợp tốt nhất(best-case), trường hợp xấu nhất(worst-case) và trường hợp trung bình của thuật toán(average-case).

#### 2.1. Ký hiệu: Ο

- Ο(n) là một cách để biểu diễn `tiệm cận trên` của thời gian chạy của một giải thuật. Qua đó, O(n) sẽ ước lượng độ phức tạp thời gian của giải thuật, trong `trường hợp xấu nhất` sẽ tương đương với số lượng thời gian chạy dài nhất cần thiết cho một giải thuật tính từ khi giải thuật được thực thi từ bắt đầu cho đến khi kết thúc.

```shell
Ο(g(n)) = { f(n) : nếu tồn tại hằng số c > 0 và n0 sao cho 0 <= f(n) <= cg(n) với mọi n > n0. }
```

- Công thức trên nghĩa là O(g(n)) chỉ một tập hợp tất cả các hàm f(n) thoả mãn điều kiện ở trên. Ví dụ, hai hàm f1(n) = n và f2(n) = n^2 + n + 1 đều thuộc O(n^2)

#### 2.2. Ký hiệu: Ω

- Ω(n) là một cách để biểu diễn `tiệm cận dưới` của thời gian chạy của một giải thuật. Qua đó, Ω(n) ước lượng độ phức tạp thời gian của giải thuật, trong `trường hợp tốt nhất` sẽ tương đương với số lượng thời gian chạy ngắn nhất cần thiết cho một giải thuật tính từ khi giải thuật được thực thi từ bắt đầu cho đến khi kết thúc.

```shell
Ω(g(n)) = { f(n) : nếu tồn tại hằng số c > 0 và n0 sao cho 0 <= cg(n) <= f(n) với mọi n > n0 }
```

#### 2.3. Ký hiệu: θ

- θ(n) là cách để biểu diễn cả `tiệm cận trên` và `tiệm cận dưới` của thời gian chạy của một giải thuật. Qua đó, θ(n) ước lượng độ phức tạp thời gian của giải thuật, `trong trường hợp trung bình` sẽ tương đương với số lượng thời gian chạy trung bình cần thiết cho một giải thuật tính từ khi giải thuật được thực thi từ bắt đầu cho đến khi kết thúc.

```shell
θ(g(n)) = { f(n) nếu và chỉ nếu f(n) =  Ο(g(n)) và f(n) = Ω(g(n)) với mọi n > n0. }
```

- Hoặc nói theo một các khác

```shell
θ(g(n)) = { f(n) nếu tồn tại các hằng số c0, c1 và n0 sao cho 0 <= c0(gn) <= f(n) <= c1g(n) với mọi n >= n0 }
```

#### 2.4. Ký hiệu: o

- O(n) với định nghĩa ở trên có thể hoặc không cần là một tiệm cận chặt. Ví dụ, ta xét O(n^2) thì n^2 là một tiệm cận chặt, còn n + 1 là một tiệm cận không chặt. o sẽ được sử dụng để định nghĩa một tiệm cận chặt, cụ thể, o được định nghĩa như sau

```shell
o(g(n)) = { f(n) nếu với một hằng số c > 0 bất kỳ, ta luôn tìm được n0 sao cho 0 <= f(n) <= cg(n) với mọi n >= n0 }
```

- Ví dụ, n^2 là o(n^2), còn 2n^2 thì không phải.

#### 2.5. Ký hiệu: ω

- Tương tự o với O, ta có ω với Ω. Từ đó ta có định nghĩa sau

```shell
ω(g(n)) = { f(n) nếu với một hằng số c > 0 bất kỳ, ta luôn tìm được n0 sao cho 0 <= cg(n) <= f(n) với mọi n >= n0 }
```

### 3. Định lý thợ(master's theorem)

- Cho hệ thức truy hồi

```shell
T(n) = aT(n/b) + f(n)
```

- Nếu af(n/b) = kf(n) với k < 1, ta có T(n) = θ(f(n))
- Nếu af(n/b) = Kf(n) với K > 1, ta có T(n) = θ(n^(logb(a)))
- Nếu af(n/b) = f(n), ta có T(n) = θ(f(n)logb(n))

### 4. Các phân tích thuật toán

- Xác định input, output, xác định các cấu trúc dữ liệu cần dùng
- Xác định các giải thuật cần dùng(đệ quy, chia để trị,...)
- Phần tích độ phức tạp của thuật toán
  - dựa vào `phương pháp thay thế`, ta đoán biên và sử dụng toán học để chứng minh giả sử đúng
  - dựa vào `phương pháp cây đệ quy`
  - sử dụng định lý thợ
