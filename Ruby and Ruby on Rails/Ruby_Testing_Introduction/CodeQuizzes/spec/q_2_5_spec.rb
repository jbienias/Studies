require 'q_2_5'

describe "#hashify" do
  it "converts to hash" do
    arr = [:a, :b, :c, :d]
    expected = {a: :b, c: :d}
    expect(arr.hashify).to eq expected
  end
end
