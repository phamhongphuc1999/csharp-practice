### 1. Problem 1: Bài toán tìm dãy con lớn nhất(Exercise 4 và exercise 5)

#### 1.1: Cách giải đệ quy(chia để trị)

- Để tìm dãy con có tổng lớn nhất, ta chia nhỏ bài toán ban đầu thành 2 bài toán nhỏ hơn có kích thức bằng một nửa bài toán đầu, lúc này thì dãy con lớn nhất sẽ có hai trường hợp xảy ra
  - Nằm hoàn toàn ở một trong hai bài toán con(1)
  - Nằm ở cả hai nửa của bài toán con(2)
- Trong trường hợp đầu tiên, ta lặp lại đệ quy bằng cách chia nhỏ tiếp bài toán con thành hai bài toán nhỏ hơn, cho đế khi dãy chỉ còn một phần tử thì rõ ràng nó chính là dãy có tổng lớn nhất => phần đệ quy này có độ phức tạp O(nlg(n))
- Trong trường hợp (2), ta chia thành hai bài toán nhỏ là tìm hai nữa lớn nhất của hai mảng bài toán con, ta dễ dàng thấy thuật toán này có độ phức tạp O(n)
- => Thuật toán đệ quy này có độ phức tạp O(nlg(n))

#### 1.2: Cách giải tuyến tính(quy hoạch động)

- Ta xem xét quan sát sau
  - Giả sử ta đã biết dãy con lớn nhất của dãy [1, j], gọi là M[1, j]
  - Đồng thời ta cũng tìm được dãy lớn nhất kết thúc bằng phần tử thứ j(gọi là E[j]) với quan sát sau:
    - Nếu j = 1 thì E[j] = [1]
    - Nếu j > 1, nếu E[j - 1] có tổng >= 0 => E[j] = E[j - 1] + j; Nếu có tổng < 0 thì E[j] = j
  - Khi ta đã biết M[1, j] và E[j + 1]; M[1, j + 1] hoặc bằng M[1, j] hoặc bằng E[j + 1]
- => Thuật toán này có độ phức tạp tuyến tính, O(n)
